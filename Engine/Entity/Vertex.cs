using OpenTK;

namespace engine.Entity
{
    public struct Vertex
    {
        public readonly Vector3 Position;

        public Vertex(Vector3 position)
        {
            Position = position;
        }
    }
}