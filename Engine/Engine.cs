using System.Collections.Generic;
using Engine.Entity;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace engine
{
    public class Engine
    {
        public GameWindow window;

        public Engine()
        {
            window = new GameWindow(600, 600, null, "Hello World");
            
            GL.ClearColor(0.05f, 0.15f, 0.3f, 1.0f);

            List<Vertex> vertices = new List<Vertex>();
            
            vertices.Add(new Vertex(new Vector3(0.5f, -0.5f, 1.0f)));
            vertices.Add(new Vertex(new Vector3(0.0f, 0.5f, 1.0f)));
            vertices.Add(new Vertex(new Vector3(-0.5f, -0.5f, 1.0f)));
            
            Mesh mesh = new Mesh(vertices);
            
            window.RenderFrame += (w, e) =>
            {
                mesh.Render();
                
                window.SwapBuffers();
                
                window.ProcessEvents();
            }; 
                
            window.Run();
        }
    }
}