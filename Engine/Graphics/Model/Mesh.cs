namespace Engine.Graphics.Model
{
    public abstract class Mesh : GlObject, IRenderable
    {
        public abstract void Render();
        protected internal abstract override void _glInitialise();
        protected internal abstract override void _glDispose();
    }
}