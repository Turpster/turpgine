using System;
using System.Collections.Generic;
using Engine.GameObject;
using Engine.GameObject._3D;
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
        public GameWindow Window;

        private Logger.Logger Logger = new Logger.Logger(
            #if DEBUG
            Level.Debug
            #else
            Level.Severe
            #endif
        );
        
        public Engine(GameWindow window)
        {
            SetupLogger();

            Window = window;

            ShaderProgram shaderProgram = new ShaderProgram();
            
            shaderProgram.Use();

            GL.ClearColor(0.05f, 0.15f, 0.3f, 1.0f);
            
            Window.RenderFrame += (w, e) =>
            {
                GL.Clear(ClearBufferMask.ColorBufferBit);
                
                Window.SwapBuffers();
                
                Window.ProcessEvents();
            }; 
                
            Window.Run();
        }

        public Engine(int width = 600, int height = 600, string title = "Open-GL Engine") : this(new GameWindow(width, height, null, title)) {}
        
        private void SetupLogger()
        {
            Logger.AddOutput(Console.Out);
            Logger.Log(Level.Debug, "You may see more logs then expected since Debug mode is on.");
            
            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                Exception targetException = (Exception) args.ExceptionObject;

                Logger.Log(Level.Error, "An unhandled exception has been thrown.", targetException);

                if (args.IsTerminating)
                {
                    Logger.Log(Level.Severe, "Program terminated by " + targetException.GetType().Name + ".");
                }
                
            };
        }
    }
}
