using System;
using System.Threading;

namespace Engine.Graphics.Scheduler
{
    public class GlCallResult : IDisposable
    {
        protected internal readonly GlCall TargetGlCall;
        protected internal readonly bool Synchronised;
        public GlCallResult(GlCall targetGlCall, bool synchronised)
        {
            TargetGlCall = targetGlCall;
            Synchronised = synchronised;
        }
        
        public readonly ManualResetEventSlim _signal = new ManualResetEventSlim(false);

        // TODO
        // Add time executed
        // How many times executed
        // etc.

        public void Dispose()
        {
            _signal.Dispose();
        }
    }
}