using System;
using System.Collections.Generic;
using System.Reflection;
using Common;
using Engine.Graphics.Scheduler;
using Logger;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Shader
{
    public class ShaderProgramManager : GlRenderHandler
    {
        // TODO Create properties to pickup current shader through OpenGL.
        private static int _currentShaderHash;
        private readonly Dictionary<int, ShaderProgram> _shaderPrograms = new Dictionary<int, ShaderProgram>();

        public ShaderProgramManager(GlMasterRenderHandler glMasterRenderHandler) : base(glMasterRenderHandler)
        { }

        public ShaderProgramManager(GlMasterRenderHandler glMasterRenderHandler, ShaderProgram shaderProgram) : base(glMasterRenderHandler)
        {
            Add(shaderProgram);
        }

        public Dictionary<int, ShaderProgram>.ValueCollection ShaderPrograms => _shaderPrograms.Values;

        protected internal void GlUse(ShaderProgram targetShaderProgram)
        {
            Turpgine.Logger.Log(Level.Debug, "Using ShaderProgram " + targetShaderProgram.GetHashCode() + ".");

            if (!_shaderPrograms.ContainsKey(targetShaderProgram.GetHashCode()))
                throw new ArgumentException("Shader Program has not been added to Hash Dictionary.");

            targetShaderProgram.Use();
            _currentShaderHash = targetShaderProgram.GetHashCode();
        }

        // TODO Ensure ShaderProgram is not bound to any other manager
        public void Add(ShaderProgram shaderProgram)
        {
            Turpgine.Logger.Log(Level.Debug, "Adding ShaderProgram " + shaderProgram.GetHashCode() + ".");
            _shaderPrograms.Add(shaderProgram.GetHashCode(), shaderProgram);
        }

        public void Remove(ShaderProgram shaderProgram)
        {
            Turpgine.Logger.Log(Level.Debug, "Removing ShaderProgram " + shaderProgram.GetHashCode() + ".");
            _shaderPrograms.Remove(shaderProgram.GetHashCode());
        }

        public override GlCallResult _glInitialise()
        {
            return GlCall(() =>
            {
                var assembly = Assembly.GetExecutingAssembly();

                var vertexShaderResource = "Engine.Graphics.Shader.GLSL.vertex-shader.vert";
                var fragmentShaderResource = "Engine.Graphics.Shader.GLSL.fragment-shader.frag";

                Turpgine.Logger.Log(Level.Debug, "Loading resource '" + vertexShaderResource + "'.");
                var vertexShader =
                    new Shader(this,
                        StreamUtil.ReadStringStream(
                            assembly.GetManifestResourceStream(vertexShaderResource)),
                        "vertex-shader.vert", ShaderType.VertexShader);

                Turpgine.Logger.Log(Level.Debug, "Loading resource '" + fragmentShaderResource + "'.");
                var fragmentShader =
                    new Shader(this,
                        StreamUtil.ReadStringStream(
                            assembly.GetManifestResourceStream(fragmentShaderResource)),
                        "fragment-shader.frag", ShaderType.FragmentShader);

                var shaderProgram = new ShaderProgram(this, vertexShader, fragmentShader);
                
                Add(shaderProgram);
                GlUse(shaderProgram);
            });
        }

        public override GlCallResult _glDispose()
        {
            return GlCall(() => { throw new NotImplementedException(); });
        }
    }
}