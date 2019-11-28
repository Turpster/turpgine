using System;

namespace Engine.Graphics.Execution
{
    public class GlAction : GlCall
    {
        protected internal readonly Action Action;

        public GlAction(Action action)
        {
            Action = action;
        }

        public override void QueueSync()
        {
            Queue();
            _signal.Wait();
        }

        public override void Queue()
        {
            GlEventHandler.GlActions.Enqueue(this);
        }
    }
}