using System.Collections.Generic;
using Logger;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Interface
{
    public class GraphicalManager : IRenderable
    {
        protected internal Dictionary<string, GraphicalInterface> _graphicalInterfaces = new Dictionary<string, GraphicalInterface>();
        
        // <Graphical Interface Name, Graphical Interface>
        public Dictionary<string, GraphicalInterface> GraphicalInterfaces => _graphicalInterfaces;
        
        private readonly GameWindow Window;
        
        public GraphicalManager(GameWindow window)
        {
            Window = window;
            
            Engine.Logger.Log(Level.Debug, "Adding RenderFrame method " + this.GetHashCode() + ".");
            Window.RenderFrame += Render;
            
            GlBackgroundColor(0.05f, 0.15f, 0.3f, 1.0f);
            
            Window.Run();
        }

        public void Render()
        {
            foreach (var graphicalInterface in GraphicalInterfaces)
            {
                graphicalInterface.Value.Render();
            }
            
            Window.SwapBuffers();
            
            Window.ProcessEvents();
        }

        private void Render(object w, FrameEventArgs e) => Render();
        
        public static void GlBackgroundColor(Vector4 vector4)
        {
            Engine.Logger.Log(Level.Debug, "Setting glBackgroundColor to " + vector4.X + " " + vector4.Y + " " + vector4.Z + " " + vector4.W + ".");
            GL.ClearColor(vector4.X, vector4.Y, vector4.Z, vector4.W); // TODO Might be wrong way round
        }
        
        public static void GlBackgroundColor(float red, float green, float blue, float alpha)
        {
            GlBackgroundColor(new Vector4(red, green, blue, alpha));
        }
    }
}