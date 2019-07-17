using engine;
using Engine.GameObject._3D;

namespace Engine.GameObject
{
    public abstract class Model : IRenderable
    {
        private ModelManager _modelManager;
        
        public Model(ModelManager modelManager)
        {
            _modelManager = modelManager;
            
            _modelManager.Add(this);
        }

        ~Model()
        {
            _modelManager.Remove(this);
        }

        public abstract void Render();
    }
}