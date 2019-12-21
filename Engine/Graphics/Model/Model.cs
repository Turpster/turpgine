using System;
using Engine.Graphics.Scheduler;

namespace Engine.Graphics.Model
{
    public abstract class Model : GlObject, IGlRenderable
    {
        protected internal readonly ModelManager _modelManager;

        public Mesh Mesh;

        protected Model(ModelManager modelManager) : base(modelManager)
        {
            _modelManager = modelManager;

            _modelManager.Add(this);
        }

        public abstract GlCallResult GlRender();

        ~Model()
        {
            _modelManager.Remove(this);
        }

        public override GlCallResult _glInitialise()
        {
            return GlRenderHandler.GlCall(() => throw new NotImplementedException());
        }

        public override GlCallResult _glDispose()
        {
            return GlRenderHandler.GlCall(() => throw new NotImplementedException());
        }
    }
}