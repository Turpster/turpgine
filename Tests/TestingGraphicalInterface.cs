using System.Collections.Generic;
using Engine.Graphics.Interface;
using Engine.Graphics.Model;
using Engine.Graphics.Model._3D;
using OpenTK;

namespace Tests
{
    public class TestingGraphicalInterface : GraphicalInterface
    {
        public TestingGraphicalInterface(GraphicalManager graphicalManager) : base(graphicalManager, "Testing Interface")
        {
            InitObjects();
        }

        private void InitObjects()
        {
            var vertices = new Vertex[]
            {
                new Vertex(new Vector3(0.5f, -0.5f, 1.0f)), //      0
                new Vertex(new Vector3(0.5f, 0.5f, 1.0f)), //       1
                new Vertex(new Vector3(-0.5f, -0.5f, 1.0f)), //     2
                new Vertex(new Vector3(-0.5f, 0.5f, 1.0f)), //      1
                new Vertex(new Vector3(0.5f, -0.5f, 1.0f)), //      2
                new Vertex(new Vector3(0.5f, 0.5f, 1.0f)) //        3
            };
            
            new Model3D(ModelManager, new Mesh3D(vertices, new uint [] {0, 1, 2, 1, 2, 3} ));
        }
    }
}