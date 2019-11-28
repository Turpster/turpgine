using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Graphics.Execution.GlEvent;

namespace Engine.Graphics.Execution
{
    public class GlEventHandler 
    {
        protected internal static Queue<GlAction> GlActions;
        protected internal static Queue<GlFunc<GlEvent.IGlEvent>> GlFuncs;
        
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
        }

        protected internal void GlEnqueue(GlAction glAction)
        {
            GlActions.Enqueue(glAction);
        }
        
        protected internal void GlEnqueue(GlFunc<GlEvent.IGlEvent> glAction)
        {
            GlFuncs.Enqueue(glAction);
        }

        public void Execute()
        {
            
        }

        public static void GlCall(Action action)
        {
            new GlAction(action).Queue();
        }

        public static void GlCallSync(Action action)
        {
            new GlAction(action).QueueSync();
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