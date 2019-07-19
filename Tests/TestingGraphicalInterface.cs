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
            var vertices = new List<Vertex>
            {
                new Vertex(new Vector3(0.5f, -0.5f, 1.0f)),
                new Vertex(new Vector3(0.0f, 0.5f, 1.0f)),
                new Vertex(new Vector3(-0.5f, -0.5f, 1.0f))
            };
            
            new Model3D(ModelManager, new Mesh3D(vertices));
        }
    }
}