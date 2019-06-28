using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace open_gl
{
    class Program
    {
        static void Main(string[] args)
        {
            
            new Window().Run();
        }
    }

    class Window : OpenTK.GameWindow
    {
        public Window() : base(500, 500, GraphicsMode.Default)
        {
            
        }

        protected override void OnRenderFrame(FrameEventArgs e)
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);
            GL.Begin(BeginMode.Triangles);

            // red apex
            GL.Color3(1.0, 0.0, 0.0);
            GL.Vertex2(-1.0, -1.0);

            // green apex
            GL.Color3(0.0, 1.0, 0.0);
            GL.Vertex2(1.0, -1.0);

            // blue apex
            GL.Color3(0.0, 0.0, 1.0);
            GL.Vertex2(0, 1.0);

            GL.End();
            SwapBuffers();
        }
    }
}