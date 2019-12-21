using System;

namespace Engine.Graphics.Scheduler
{
    public abstract class GlCall : IDisposable
    {
        protected readonly GlRenderHandler GlRenderHandler;
        
        public bool Recursive;

        protected internal GlCallResult _result;
        public GlCallResult Result => _result;
        
        internal GlCall(GlRenderHandler glRenderHandler, bool synchronised, bool recursive=false)
        {
            GlRenderHandler = glRenderHandler;
            Recursive = recursive;
            _result = new GlCallResult(this, synchronised);
        }

        public void Dispose()
        {
            Result.Dispose();
        }
        
        public abstract void Queue();
    }
}