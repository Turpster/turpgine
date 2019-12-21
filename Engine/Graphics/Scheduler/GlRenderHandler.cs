using System;
using System.Collections.Generic;
using System.Threading;
using Engine.Graphics.Scheduler.GlEvent;
using Logger;

namespace Engine.Graphics.Scheduler
{
    public abstract class GlRenderHandler : GlInitialisable, IGlRenderable
    {

        protected Queue<GlAction> _glActions = new Queue<GlAction>();
        protected Queue<GlFunc<IGlEvent>> _glFuncs = new Queue<GlFunc<IGlEvent>>();

        protected ManualResetEventSlim _glActionsBufferSignal = new ManualResetEventSlim();
        protected Queue<GlAction> _glActionsBuffer = new Queue<GlAction>();

        protected ManualResetEventSlim _glFuncsBufferSignal = new ManualResetEventSlim();
        protected Queue<GlFunc<IGlEvent>> _glFuncsBuffer = new Queue<GlFunc<IGlEvent>>();

        protected Dictionary<GlObject, GlAction> UninitGlObjects = new Dictionary<GlObject, GlAction>(new GlObjectEqualityComparer());

        private GlMasterRenderHandler MasterRenderHandler;
        
        protected List<GlRenderHandler> RenderHandlers = new List<GlRenderHandler>();
        protected List<GlRenderHandler> UninitRenderHandlers = new List<GlRenderHandler>();

        public GlRenderHandler(GlRenderHandler glRenderHandler)
        {
            glRenderHandler.AddUnitGlRenderHandler(this);
            _glInitialise();
        }

        protected internal GlRenderHandler()
        {
            _glInitialise();
        }

        public GlCallResult GlRender()
        {
            return _glExecuteSync(new GlAction(() =>
            {
                List<GlFunc<IGlEvent>> recursiveGlFuncs = new List<GlFunc<IGlEvent>>(); 
                
                _glFuncsBufferSignal.Reset();
                foreach (var glFunc in _glFuncsBuffer)
                {
                    _glFuncs.Enqueue(glFunc);
                    if (glFunc.Recursive) recursiveGlFuncs.Add(glFunc);
                }
                _glFuncsBuffer.Clear();
                foreach (var recursiveGlFunc in recursiveGlFuncs)
                {
                    _glFuncsBuffer.Enqueue(recursiveGlFunc);
                }
                recursiveGlFuncs.Clear();
                
                _glFuncsBufferSignal.Set();
            
                List<GlAction> recursiveGlActions = new List<GlAction>(); 
                
                _glActionsBufferSignal.Reset();
                foreach (var glAction in _glActionsBuffer)
                {
                    _glActions.Enqueue(glAction);
                    if (glAction.Recursive) recursiveGlActions.Add(glAction);
                }
                _glActionsBuffer.Clear();
                foreach (var recursiveGlAction in recursiveGlActions)
                {
                    _glActionsBuffer.Enqueue(recursiveGlAction);
                }
                recursiveGlFuncs.Clear();
                _glActionsBufferSignal.Set();
            
                foreach (var glAction in _glActions)
                {
                    _glExecuteSync(glAction);
                }
                _glActions.Clear();
                foreach (var glFunc in _glFuncs)
                {
                    glFunc.Result._value = glFunc.Function();
                    glFunc.Result._signal.Set();
                }
                _glFuncs.Clear();

                List<GlObject> initGlObjects = new List<GlObject>();
            
                foreach (var uninitGlObject in UninitGlObjects)
                {
                    if (uninitGlObject.Key is null) continue;

                    _glExecuteSync(uninitGlObject.Value);
                    uninitGlObject.Key.GlInitialised = true;
                    initGlObjects.Add(uninitGlObject.Key);
                }
            
                foreach (var initObject in initGlObjects)
                {
                    UninitGlObjects.Remove(initObject);
                }
                
                List<GlRenderHandler> initGlRenderHandlers = new List<GlRenderHandler>();
                
                for (int i = 0; i < UninitRenderHandlers.Count; i++)
                {
                    var glRenderHandler = UninitRenderHandlers[i];
                    if (glRenderHandler is null) continue;
                    
                    glRenderHandler.GlRender();
                    glRenderHandler.GlInitialised = true;
                    initGlRenderHandlers.Add(glRenderHandler);
                }

                for (int i = 0; i < initGlRenderHandlers.Count; i++)
                {
                    var initGlRenderHandler = initGlRenderHandlers[i];
                    initGlRenderHandlers.Remove(initGlRenderHandler);
                }
            }, this, false, true));
        }

        protected internal void AddUnitGlObject(GlObject glObject, GlAction glAction)
        {
            #if DEBUG
            if (glObject is null)
            {
                throw new ArgumentNullException(nameof(glObject), "Argument can not be null.");
            }
            if (glAction is null)
            {
                throw new ArgumentNullException(nameof(glAction), "Argument can not be null.");
            }
            #endif
            
            UninitGlObjects.Add(glObject, glAction);
        }

        protected internal void AddUnitGlRenderHandler(GlRenderHandler glRenderHandler)
        {
            #if DEBUG
            if (glRenderHandler is null)
            {
                throw new ArgumentNullException(nameof(glRenderHandler), "Argument can not be null.");
            }
            #endif
            
            UninitRenderHandlers.Add(glRenderHandler);
        }
        
        protected internal void GlEnqueue(GlAction glAction)
        {
            if (!_glActionsBufferSignal.IsSet)
                _glActionsBuffer.Enqueue(glAction);
        }
        
        protected internal void GlEnqueue(GlFunc<IGlEvent> glAction)
        {
            if (!_glActionsBufferSignal.IsSet)
                _glFuncsBuffer.Enqueue(glAction);
        }

        public GlCallResult GlCall(Action action, bool recursive=false)
        {
            var glAction = new GlAction(action, this, false, recursive);
            glAction.Queue();
            return glAction._result;
        }
        
        public GlCallResult GlCallSync(Action action, bool recursive=false)
        {
            GlAction glAction = new GlAction(action, this, true, recursive);

            if (Thread.CurrentThread.Name == GlMasterRenderHandler.GlContextThreadName)
            {
                glAction.Action();
            }
            else
            {
                glAction.Queue();
                glAction._result._signal.Wait();
            }
            
            return glAction._result;
        }
        
        
        public GlFuncResult<T> GlCall<T>(Func<T> func, bool recursive=false)
        where T : IGlEvent
        {
            var glFunc = new GlFunc<T>(func, this, false, recursive);

            return _glExecute(glFunc);
        }

        public GlFuncResult<T> GlCallSync<T>(Func<T> func, bool recursive=false)
            where T : IGlEvent 
        {
            var glFunc = new GlFunc<T>(func, this, true, recursive);
            if (Thread.CurrentThread.Name == GlMasterRenderHandler.GlContextThreadName)
            {
                return _glExecuteSync(glFunc);
            }
            else
            {
                return _glExecute(glFunc);
            }
        }

        protected internal static GlCallResult _glExecute(GlAction glAction)
        {
            glAction.Queue();
            return glAction.Result;
        }
        
        protected internal static GlFuncResult<T> _glExecute<T>(GlFunc<T> glFunc)
            where T : IGlEvent
        {
            glFunc.Queue();
            return glFunc.Result;
        }

        protected internal static GlCallResult _glExecuteSync(GlAction glAction)
        {
            glAction.Action();
            glAction._result._signal.Set();
            return glAction.Result;
        }
        
        protected internal static GlFuncResult<T> _glExecuteSync<T>(GlFunc<T> glFunc)
            where T : IGlEvent
        {
            glFunc._result._value = glFunc.Function();
            return glFunc.Result;
        }
    }
}