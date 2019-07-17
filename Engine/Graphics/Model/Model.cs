namespace Engine.Graphics.Model
{
    public abstract class Model : IRenderable
    {
        private readonly ModelManager _modelManager;

        protected Model(ModelManager modelManager)
        {
            _modelManager = modelManager;

            _modelManager.Add(this);
        }

        public abstract void Render();

        ~Model()
        {
            _modelManager.Remove(this);
        }
    }
}