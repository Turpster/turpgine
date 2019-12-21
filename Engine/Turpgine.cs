using System;
using Engine.Graphics.Interface;
using Engine.Graphics.Scheduler;
using Logger;

namespace Engine
{
    public class Turpgine
    {
        public static readonly Logger.Logger Logger = new Logger.Logger(
            #if DEBUG
            Level.Debug
            #else
            Level.Severe
            #endif
        );

        internal readonly GraphicalInterfaceManager GraphicalInterfaceManager;
        internal readonly GlMasterRenderHandler glMasterRenderHandler;

        public Turpgine()
        {
            SetupLogger();
            glMasterRenderHandler = new GlMasterRenderHandler(800, 600, "Hello, World!");

            Logger.Log(Level.Debug, "Creating new GraphicalManager object.");
            GraphicalInterfaceManager = new GraphicalInterfaceManager(glMasterRenderHandler);
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