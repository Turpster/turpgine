using System;

namespace Engine.Graphics
{
    public abstract class GlObject : IDisposable
    {
        public bool GlInitialised = false;
        
        protected internal void GlInitialise()
        {
            GlInitialised = true;
            _glInitialise();
        }

        protected internal void GlDispose()
        {
            GlInitialised = false;
            _glDispose();
        }
        
        protected internal abstract void _glInitialise();
        protected internal abstract void _glDispose();

        public void Dispose() => _glDispose();
    }
}