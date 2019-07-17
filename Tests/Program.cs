using System.Collections.Generic;
using Engine.GameObject;
using Engine.GameObject._3D;
using OpenTK;

namespace Tests
{
    class Program
    {
        static void Main(string[] args)
        {
            List<Vertex> vertices = new List<Vertex>
            {
                new Vertex(new Vector3(0.5f, -0.5f, 1.0f)),
                new Vertex(new Vector3(0.0f, 0.5f, 1.0f)),
                new Vertex(new Vector3(-0.5f, -0.5f, 1.0f))
            };

            Mesh3D mesh3D = new Mesh3D(vertices);

            engine.Engine engine = new engine.Engine();
        }
    }
}