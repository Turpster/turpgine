using System;

namespace Engine.Graphics.Scheduler
{
    public class GlAction : GlCall
    {
        protected internal readonly Action Action;
        
        internal GlAction(Action action, GlRenderHandler glRenderHandler, bool synchronised, bool recursive=false) : base(glRenderHandler, synchronised, recursive)
        {
            Action = action;
        }

        public override void Queue()
        {
            GlRenderHandler.GlEnqueue(this);
        }
    }
}