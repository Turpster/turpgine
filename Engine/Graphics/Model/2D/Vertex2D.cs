using OpenTK;

namespace Engine.Graphics.Model._2D
{
    public struct Vertex2D
    {
        /*
         * TODO 2D _positions may cause issues with 3D shaders as 3D shaders may require 3 floats
         * Fix this by adding 2D shaders
         */
        private Vector2 _position;
        
        public Vertex2D(Vector2 position1)
        {
            _position = position1;
        }
    }
}