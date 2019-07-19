namespace Engine.Graphics.Model
{
    public abstract class Model : IRenderable
    {
        private readonly ModelManager _modelManager;

        protected Mesh _mesh;
        
        protected Model(ModelManager modelManager, Mesh mesh)
        {
            _modelManager = modelManager;

            _mesh = mesh;
            
            _modelManager.Add(this);
        }

        public abstract void Render();

        ~Model()
        {
            _modelManager.Remove(this);
        }

        public void GlInit()
        {
            _mesh.GlInit();
        }
    }
}