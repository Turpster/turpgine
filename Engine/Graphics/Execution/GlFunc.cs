using System;
using System.Threading;

namespace Engine.Graphics.Execution
{

    public class GlFunc<T> : GlCall
    where T : GlEvent.IGlEvent
    {
        protected internal readonly Func<T> Function;

        protected internal T result;
        
        public T Result
        {
            get
            {
                _signal.Wait();
                return result;
            }
        }
        
        public GlFunc(Func<T> function)
        {
            Function = function;
        }
        
        protected internal ManualResetEventSlim _signal = new ManualResetEventSlim(false);

        public override void QueueSync()
        {
            Queue();
            _signal.Wait();
        }

        public override void Queue()
        {
            GlEventHandler.GlFuncs.Enqueue(this as GlFunc<GlEvent.IGlEvent>);
        }
    }
}