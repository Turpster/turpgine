namespace Engine.Entity
{
    public class Entity
    {
        private Mesh Mesh;

        public Entity(Mesh mesh)
        {
            Mesh = mesh;
        }

        public void Render()
        {
            Mesh.Render();
        }
    }
}