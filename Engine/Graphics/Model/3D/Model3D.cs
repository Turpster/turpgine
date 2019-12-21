using Engine.Graphics.Scheduler;

namespace Engine.Graphics.Model._3D
{
    public class Model3D : Model
    {
        public Model3D(ModelManager modelManager) : base(modelManager)
        {
        }

        public override GlCallResult GlRender()
        {
            return GlRenderHandler.GlCall(() =>
            {
                Mesh.GlRender();
            }, true);
        }

        public override GlCallResult _glInitialise()
        {
            return GlRenderHandler.GlCall(() => { });
        }

        public override GlCallResult _glDispose()
        {
            return GlRenderHandler.GlCall(() => { });
        }
    }
} 