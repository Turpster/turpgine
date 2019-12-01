using Engine.Graphics.Execution;

namespace Engine.Graphics.Model
{
    public abstract class Model : GlObject, IRenderable
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
        protected override GlAction _glInitialise()
        {
            return new GlAction(() => throw new NotImplementedException());
        }

        protected override GlAction _glDispose()
        {
            return new GlAction(() => throw new NotImplementedException());
        }
    }
}