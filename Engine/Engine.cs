using System;
using System.Collections.Generic;
using System.Net.Mime;
using System.Threading;
using Engine.Entity;
using log4net.Core;
using log4net.Repository.Hierarchy;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace engine
{
    public class Engine
    {
        public static Logger Logger { get; } = new RootLogger 
        #if DEBUG
            (Level.Debug);
        #else            
            (Level.Fine);
        #endif

        static Engine engine;
        
        public GameWindow window;

        public Engine()
        {
            SetupLogger();
            
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

        private void SetupLogger()
        {
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Exception targetException = (Exception) args.ExceptionObject;
                
                Logger.Log(Level.Error, "An unhandled exception has been thrown.", targetException);
            };
        }
    }
}