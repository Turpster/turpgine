using System;

namespace Engine.Graphics
{
    public abstract class GlObject : IDisposable
    {
        protected internal abstract void GlInitialise();
        protected internal abstract void GlDispose();

        public void Dispose() => GlDispose();
    }
}