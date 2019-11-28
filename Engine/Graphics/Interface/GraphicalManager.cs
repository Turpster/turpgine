using System;
using System.Collections.Generic;
using System.Threading;
using Engine.Graphics.Execution;
using Engine.Graphics.Shader;
using Logger;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Interface
{
    public class GraphicalManager : GlObject, IRenderable
    {
        public static readonly List<Action> GlActions = new List<Action>();

        protected internal Dictionary<string, GraphicalInterface> _graphicalInterfaces =
            new Dictionary<string, GraphicalInterface>();

        public ShaderProgramManager ShaderProgramManager;

        private GameWindow Window;

        public GraphicalManager(GameWindow window)
        {
            Window = window;

            Engine.Logger.Log(Level.Debug, "Creating new ShaderProgramManager object.");
            ShaderProgramManager = new ShaderProgramManager();

            var thread = new Thread(_glInitialise);
            thread.Start();
        }

        // <Graphical Interface Name, Graphical Interface>
        public Dictionary<string, GraphicalInterface> GraphicalInterfaces => _graphicalInterfaces;

        public void Render()
        {
            GL.Clear(ClearBufferMask.ColorBufferBit);

            foreach (var graphicalInterface in GraphicalInterfaces) graphicalInterface.Value.Render();

            Window.SwapBuffers();

            Window.ProcessEvents();
        }

        protected internal override void _glInitialise()
        {
            Window = new GameWindow(Window.Width, Window.Height, null, Window.Title);

            Engine.Logger.Log(Level.Debug, "Adding RenderFrame method " + GetHashCode() + ".");
            Window.RenderFrame += Render;
            Window.RenderFrame += ExecuteGlActions;

            ShaderProgramManager._glInitialise();

            foreach (var graphicalInterface in GraphicalInterfaces) graphicalInterface.Value._glInitialise();

            Window.Run();
        }

        private void ExecuteGlActions()
        {
            foreach (var action in GlActions) action();

            GlActions.Clear();
        }

        private void ExecuteGlActions(object w, FrameEventArgs e)
        {
            ExecuteGlActions();
        }

        protected internal override void _glDispose()
        {
            foreach (var graphicalInterface in GraphicalInterfaces) graphicalInterface.Value._glDispose();

            _graphicalInterfaces.Clear();

            Window.Exit();
        }

        private void Render(object w, FrameEventArgs e)
        {
            Render();
        }

        public static void GlBackgroundColor(Vector4 vector4)
        {
            Engine.Logger.Log(Level.Debug,
                "Setting glBackgroundColor to " + vector4.X + " " + vector4.Y + " " + vector4.Z + " " + vector4.W +
                ".");
            GL.ClearColor(vector4.X, vector4.Y, vector4.Z, vector4.W);
        }

        public static void GlBackgroundColor(float red, float green, float blue, float alpha)
        {
            GlBackgroundColor(new Vector4(red, green, blue, alpha));
        }
    }
}