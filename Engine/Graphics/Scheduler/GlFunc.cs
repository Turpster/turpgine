using System;
using Engine.Graphics.Scheduler.GlEvent;

namespace Engine.Graphics.Scheduler
{

    public class GlFunc<T> : GlCall
    where T : IGlEvent
    {
        protected internal readonly Func<T> Function;

        protected internal new GlFuncResult<T> _result;
        
        public new GlFuncResult<T> Result => _result;

        internal GlFunc(Func<T> function, GlRenderHandler glRenderHandler, bool synchronised, bool recursive=false) : base(glRenderHandler, synchronised, recursive)
        {
            Function = function;
            
            // TODO may cause synchronisation errors as this object has not been created yet in another thread.
            _result = new GlFuncResult<T>(this, synchronised);
        }

        public override void Queue()
        {
            GlRenderHandler.GlEnqueue(this as GlFunc<IGlEvent>);
        }
    }
}