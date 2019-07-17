using Engine.Graphics.Model;

namespace Engine.Graphics.Interface
{
    public abstract class GraphicalInterface : IRenderable
    {
        protected readonly ModelManager ModelManager = new ModelManager();

        public abstract void Render();
    }
}