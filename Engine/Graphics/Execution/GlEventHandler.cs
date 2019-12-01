using System;
using System.Collections.Generic;
using Engine.Graphics.Execution.GlEvent;

namespace Engine.Graphics.Execution
{
    public class GlEventHandler 
    {
        protected internal static Queue<GlAction> GlActions = new Queue<GlAction>();
        protected internal static Queue<GlFunc<IGlEvent>> GlFuncs = new Queue<GlFunc<IGlEvent>>();

        protected internal static Dictionary<GlObject, GlAction> UninitGlObjects = new Dictionary<GlObject, GlAction>(new GlObjectEqualityComparer());
        
        protected internal void GlRender()
        {
            foreach (var glFunc in GlFuncs)
            {
                glFunc.result = glFunc.Function();
                glFunc._signal.Set();
            }
            foreach (var glAction in GlActions)
            {
                glAction.Action();
                glAction._signal.Set();
            }
            
            List<GlObject> initGlObjects = new List<GlObject>();
            
            foreach (var uninitGlObject in UninitGlObjects)
            {
                if (uninitGlObject.Key != null)
                {
                    uninitGlObject.Value.Queue();
                    initGlObjects.Add(uninitGlObject.Key);
                }
            }

            foreach (var initObject in initGlObjects)
            {
                UninitGlObjects.Remove(initObject);
            }
        }

        protected internal void GlEnqueue(GlAction glAction)
        {
            GlActions.Enqueue(glAction);
        }
        
        protected internal void GlEnqueue(GlFunc<GlEvent.IGlEvent> glAction)
        {
            GlFuncs.Enqueue(glAction);
        }

        public static void GlCall(Action action)
        {
            new GlAction(action).Queue();
        }
        public static void GlCall(GlAction action)
        {
            action.Queue();
        }

        public static void GlCallSync(Action action)
        {
            new GlAction(action).QueueSync();
        }
        
        public static void GlCallSync(GlAction action)
        {
            action.QueueSync();
        }
        
        public static void GlCall<T>(Func<T> func)
        where T : IGlEvent
        {
            new GlFunc<T>(func).Queue();
        }
        

        public static T GlCallSync<T>(Func<T> func)
            where T : IGlEvent
        {
            var glFunc = new GlFunc<T>(func);
            glFunc.Queue();
            return glFunc.Result;
        }

    }
}