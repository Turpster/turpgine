using engine;
using Engine.GameObject;

namespace Engine.GraphicalInterface
{
    public abstract class GraphicalInterface : IRenderable
    {
        protected ModelManager ModelManager = new ModelManager();

        public GraphicalInterface()
        {
            
        }

        public void Render()
        {
            
        } 
    }
}