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
        public GlEventHandler GlEventHandler;

        protected internal Dictionary<string, GraphicalInterface> _graphicalInterfaces =
            new Dictionary<string, GraphicalInterface>();

        public ShaderProgramManager ShaderProgramManager;

        private GameWindow _window;

        public GraphicalManager(GameWindow window)
        {
            _window = window;

            GlEventHandler = new GlEventHandler();
            
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
            
            GlEventHandler.GlRender();
            
            _window.SwapBuffers();

            _window.ProcessEvents();
        }

        protected internal override GlAction _glInitialise()
        {
            return new GlAction(() =>
            {
                _window = new GameWindow(_window.Width, _window.Height, null, _window.Title);

                Engine.Logger.Log(Level.Debug, "Adding RenderFrame method " + GetHashCode() + ".");
                _window.RenderFrame += Render;

                ShaderProgramManager._glInitialise();

                foreach (var graphicalInterface in GraphicalInterfaces) graphicalInterface.Value._glInitialise();

                _window.Run();
            });
        }

        protected internal override GlAction _glDispose()
        {
            return new GlAction(() =>
            {
                foreach (var graphicalInterface in GraphicalInterfaces) graphicalInterface.Value._glDispose();

                _graphicalInterfaces.Clear();

                _window.Exit();
            });
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