namespace Engine.GameObject
{
    public class GameObject
    {
        private Mesh Mesh;

        public GameObject(Mesh mesh)
        {
            Mesh = mesh;
        }

        public void Render()
        {
            Mesh.Render();
        }
    }
}