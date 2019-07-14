using System;
using System.Collections.Generic;
using System.IO;
using System.Reflection;
using Common;
using Engine.Entity;
using Engine.Shader;
using Logger;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;
using ShaderType = OpenTK.Graphics.OpenGL4.ShaderType;

namespace engine
{
    public class Engine
    {
        static Engine engine;
        
        public GameWindow window;

        private Logger.Logger logger = new Logger.Logger(
            #if DEBUG
            Level.Debug
            #else
            Level.Severe
            #endif
            );
        
        public Engine()
        {
            SetupLogger();
            
            window = new GameWindow(600, 600, null, "Hello World");
            
            GraphicsContext.CurrentContext.ErrorChecking = true;

            Assembly assembly = Assembly.GetExecutingAssembly();

            ShaderManager shaderManager = new ShaderManager( 
                new Shader(StreamUtil.ReadStringStream(assembly.GetManifestResourceStream("Engine.Shader.GLSL.vertex-shader.vert")), "vertex-shader.vert", ShaderType.VertexShader),
                new Shader(StreamUtil.ReadStringStream(assembly.GetManifestResourceStream("Engine.Shader.GLSL.fragment-shader.frag")), "fragment-shader.frag", ShaderType.FragmentShader)
            );
            
            shaderManager.Use();

            List<Vertex> vertices = new List<Vertex>
            {
                new Vertex(new Vector3(0.5f, -0.5f, 1.0f)),
                new Vertex(new Vector3(0.0f, 0.5f, 1.0f)),
                new Vertex(new Vector3(-0.5f, -0.5f, 1.0f))
            };


            Mesh mesh = new Mesh(vertices);
            
            GL.ClearColor(0.05f, 0.15f, 0.3f, 1.0f);
            
            window.RenderFrame += (w, e) =>
            {
                GL.Clear(ClearBufferMask.ColorBufferBit);

                mesh.Render();
                
                window.SwapBuffers();
                
                window.ProcessEvents();
            }; 
                
            window.Run();
        }
        
        private void SetupLogger()
        {
            logger.AddOutput(Console.Out);
            logger.Log(Level.Debug, "This is a debug test.");
            
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Exception targetException = (Exception) args.ExceptionObject;

                logger.Log(Level.Error, "An unhandled exception has been thrown.", targetException);

                if (args.IsTerminating)
                {
                    logger.Log(Level.Severe, "Program terminated by " + targetException.GetType().Name + ".");
                }
                
            };
        }
    }
}
