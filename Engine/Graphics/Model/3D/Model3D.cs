namespace Engine.GameObject._3D
{
    public class Model3D : Model
    {
        private Mesh3D _mesh;
        
        public Model3D(ModelManager modelManager, Mesh3D mesh) : base(modelManager)
        {
            _mesh = mesh;
        }

        public override void Render()
        {
            _mesh.Render();
        }
    }
}