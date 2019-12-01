namespace Engine.Graphics.Model._3D
{
    public class Model3D : Model
    {
        public Model3D(ModelManager modelManager, Mesh3D mesh) : base(modelManager, mesh)
        {
        }

        public override void Tick()
        {
            _mesh.Tick();
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