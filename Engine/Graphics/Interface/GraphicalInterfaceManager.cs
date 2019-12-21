using System;
using System.Collections.Generic;
using System.Threading;
using Engine.Graphics.Scheduler;
using Engine.Graphics.Shader;
using Logger;
using OpenTK;
using OpenTK.Graphics;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Interface
{
    public class GraphicalInterfaceManager : GlRenderHandler, IGlRenderable
    {
        protected internal GlMasterRenderHandler GlMasterRenderHandler;

        public GraphicalInterfaceManager(GlMasterRenderHandler glMasterRenderHandler) : base(glMasterRenderHandler)
        {
            GlMasterRenderHandler = glMasterRenderHandler;
        }
        
        protected internal Dictionary<string, GraphicalInterface> _graphicalInterfaces =
            new Dictionary<string, GraphicalInterface>();

        public ShaderProgramManager ShaderProgramManager;

        // <Graphical Interface Name, Graphical Interface>
        public Dictionary<string, GraphicalInterface> GraphicalInterfaces => _graphicalInterfaces;

        public GlCallResult GlRender()
        {
            return GlCall(() => { }, true);
        }

        public override GlCallResult _glInitialise()
        {
            return GlCall(() =>
            {
                
            });
        }

        public override GlCallResult _glDispose()
        {
            return GlCall(() =>
            {
                _graphicalInterfaces.Clear();
            });
        }

        private void Render(object w, FrameEventArgs e)
        {
            GlRender();
        }

        public static void GlBackgroundColor(Vector4 vector4)
        {
            Turpgine.Logger.Log(Level.Debug,
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