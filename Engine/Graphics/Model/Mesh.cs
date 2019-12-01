using Engine.Graphics.Execution;

namespace Engine.Graphics.Model
{
    public abstract class Mesh : GlObject, IRenderable
    {
        protected abstract override GlAction _glInitialise();
        protected abstract override GlAction _glDispose();
        public abstract void Tick();
    }
}