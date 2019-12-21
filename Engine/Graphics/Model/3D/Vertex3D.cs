using OpenTK;

namespace Engine.Graphics.Model._3D
{
    public struct Vertex3D
    {
        private Vector3 _position;
        
        public Vertex3D(Vector3 position)
        {
            _position = position;
        }
    }
}