using System.Collections.Generic;
using Engine.Graphics.Model;
using Engine.Graphics.Model._3D;
using OpenTK;

namespace Tests
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var vertices = new List<Vertex>
            {
                new Vertex(new Vector3(0.5f, -0.5f, 1.0f)),
                new Vertex(new Vector3(0.0f, 0.5f, 1.0f)),
                new Vertex(new Vector3(-0.5f, -0.5f, 1.0f))
            };

            var mesh3D = new Mesh3D(vertices);

            var engine = new Engine.Engine();
        }
    }
}