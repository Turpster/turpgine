using Engine.Graphics.Execution;

namespace Engine.Graphics.Model
{
    public abstract class Mesh : GlObject, IRenderable
    {
        public abstract void Render();
        protected internal abstract override GlAction _glInitialise();
        protected internal abstract override GlAction _glDispose();
    }
}