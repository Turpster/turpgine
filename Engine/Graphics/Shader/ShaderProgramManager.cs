using System.Collections.Generic;
using System.Reflection;
using Common;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Shader
{
    public class ShaderProgramManager
    {
        private readonly Dictionary<int, ShaderProgram> _shaderPrograms = new Dictionary<int, ShaderProgram>();

        public Dictionary<int, ShaderProgram>.ValueCollection ShaderPrograms => _shaderPrograms.Values;
        
        public ShaderProgramManager()
        {
            var assembly = Assembly.GetExecutingAssembly();

            Shader vertexShader =
                new Shader(
                    StreamUtil.ReadStringStream(
                        assembly.GetManifestResourceStream("Engine.Shader.GLSL.vertex-shader.vert")),
                    "vertex-shader.vert", ShaderType.VertexShader);
            Shader fragmentShader =
                new Shader(
                    StreamUtil.ReadStringStream(
                        assembly.GetManifestResourceStream("Engine.Shader.GLSL.fragment-shader.frag")),
                    "fragment-shader.frag", ShaderType.FragmentShader);

            ShaderProgram shaderProgram = new ShaderProgram(vertexShader, fragmentShader);
            
            _shaderPrograms.Add(shaderProgram.GetHashCode(), shaderProgram);
        }

        public void Use(ShaderProgram targetShaderProgram)
        {
            foreach (var shaderProgram in _shaderPrograms)
            {
                
            }
        }

        public void Add(ShaderProgram shaderProgram)
        {
            
        }

        public void Remove(ShaderProgram shaderProgram)
        {
            
        }
    }
}