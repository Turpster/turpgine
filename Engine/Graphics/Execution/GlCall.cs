using System;
using System.Threading;

namespace Engine.Graphics.Execution
{
    public abstract class GlCall : IDisposable
    {
        public readonly ManualResetEventSlim _signal = new ManualResetEventSlim();

        public void Dispose()
        {
            _signal.Dispose();
        }

        public abstract void QueueSync();
        public abstract void Queue();
    }
}