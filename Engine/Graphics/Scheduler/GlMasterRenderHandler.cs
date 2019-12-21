using System.Collections.Generic;
using System.Threading;
using Engine.Graphics.Interface;
using Engine.Graphics.Scheduler.GlEvent;
using Logger;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Scheduler
{
    public sealed class GlMasterRenderHandler : GlRenderHandler
    {
        private GameWindow _window;

        private int _windowWidth;
        private int _windowHeight;
        private string _windowTitle;

        private Thread _glContext;

        public const string GlContextThreadName = "GlContext";

        public GlMasterRenderHandler(int width, int height, string windowTitle)
        {
            // TODO More efficient way of not adding the MasterRenderHandler
            RenderHandlers.Clear();
            
            _windowHeight = height;
            _windowWidth = width;
            _windowTitle = windowTitle;

            Turpgine.Logger.Log(Level.Debug, "Creating new ShaderProgramManager object.");

            _glContext = new Thread(() => Start()) {Name = GlContextThreadName};
            _glContext.Start();
        }
        
        public void GlRender()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

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
                
                uninitGlObject.Value.Queue();
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

            _window.SwapBuffers();

            _window.ProcessEvents();
        }

        public override GlCallResult _glInitialise()
        {
            return new GlCallResult(new GlAction(() => {}, this, false), false);
        }

        public void Start()
        {
            _window = new GameWindow(_windowWidth, _windowHeight, GraphicsMode.Default, _windowTitle);
        
            Turpgine.Logger.Log(Level.Debug, "Adding RenderFrame method " + GetHashCode() + ".");
            _window.RenderFrame += (sender, args) => GlRender();
    
            _window.Run();
        }

        public override GlCallResult _glDispose()
        {
            return GlCall(() =>
            {
                _window.Close();
                _window.Dispose();
            });
        }
    }
}