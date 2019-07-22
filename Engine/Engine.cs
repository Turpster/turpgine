using System;
using Engine.Graphics.Interface;
using Logger;
using OpenTK;

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

        public readonly GraphicalManager GraphicalManager;

        public readonly GameWindow Window;

        public Engine(GameWindow window)
        {
            SetupLogger();

            Window = window;

            Logger.Log(Level.Debug, "Creating new GraphicalManager object.");
            GraphicalManager = new GraphicalManager(Window);
        }

        public Engine(int width = 600, int height = 600, string title = "Open-GL Engine") : this(new GameWindow(width,
            height, null, title))
        {
        }

        private void SetupLogger()
        {
            Logger.AddOutput(Console.Out);
            Logger.Log(Level.Info, "Logger has been initialized.");
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