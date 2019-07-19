namespace Engine.Graphics.Model._3D
{
    public class Model3D : Model
    {
        public Model3D(ModelManager modelManager, Mesh3D mesh) : base(modelManager, mesh)
        { }

        public override void Render()
        {
            _mesh.Render();
        }
    }
}