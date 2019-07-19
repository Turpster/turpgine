namespace Engine.Graphics.Model
{
    public abstract class Mesh : IRenderable
    {
        public abstract void GlInit();
        public abstract void Render();
    }
}