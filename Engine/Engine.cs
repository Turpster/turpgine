using System;
using Engine.Graphics.Interface;
using Engine.Graphics.Shader;
using Logger;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine
{
    public class Engine
    {
        public static readonly Logger.Logger Logger = new Logger.Logger(
            #if DEBUG
            Level.Debug
            #else
            Level.Severe
            #endif
        );

        public GameWindow Window;
        
        public GraphicalInterfaceManager graphicalInterfaceManager;
        
        public Engine(GameWindow window)
        {

            SetupLogger();

            Window = window;

            GL.ClearColor(0.05f, 0.15f, 0.3f, 1.0f);

            Window.RenderFrame += (w, e) =>
            {
                graphicalInterfaceManager.Render();
                
                Window.SwapBuffers();

                Window.ProcessEvents();
            };

            Window.Run();
        }

        public Engine(int width = 600, int height = 600, string title = "Open-GL Engine") : this(new GameWindow(width,
            height, null, title))
        {
        }

        private void SetupLogger()
        {
            Logger.AddOutput(Console.Out);
            Logger.Log(Level.Debug, "You may see more logs then expected since Debug mode is on.");

            AppDomain.CurrentDomain.UnhandledException += (sender, args) =>
            {
                var targetException = (Exception) args.ExceptionObject;

                Logger.Log(Level.Error, "An unhandled exception has been thrown.", targetException);

                if (args.IsTerminating)
                    Logger.Log(Level.Severe, "Program terminated by " + targetException.GetType().Name + ".");
            };
        }
    }
}