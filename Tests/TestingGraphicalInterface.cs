using Engine;
using Engine.Graphics.Interface;
using Engine.Graphics.Model._3D;
using Engine.Graphics.Scheduler;
using OpenTK;

namespace Tests
{
    public class TestingGraphicalInterface : GraphicalInterface
    {
        public TestingGraphicalInterface(Turpgine turpgine) : base(turpgine,
            "Testing Interface")
        {
            
        }

        private void InitModels()
        {
            var vertices = new[]
            {
                new Vertex3D(new Vector3(0.5f, -0.5f, 1.0f)), //      0
                new Vertex3D(new Vector3(0.5f, 0.5f, 1.0f)), //       1
                new Vertex3D(new Vector3(-0.5f, -0.5f, 1.0f)), //     2
                new Vertex3D(new Vector3(-0.5f, 0.5f, 1.0f)), //      1
                new Vertex3D(new Vector3(0.5f, -0.5f, 1.0f)), //      2
                new Vertex3D(new Vector3(0.5f, 0.5f, 1.0f)) //        3
            };

            var model = new Model3D(ModelManager);
            model.Mesh = new Mesh3D(model, vertices, new uint [] {0, 1, 2, 1, 2, 3});
        }
        
        public override GlCallResult _glInitialise()
        {
            return GlCall(() => { InitModels(); });
        }

        public override GlCallResult _glDispose()
        {
            throw new System.NotImplementedException();
        }
    }
}