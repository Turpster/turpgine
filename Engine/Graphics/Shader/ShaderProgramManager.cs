using System.Collections.Generic;
using System.Reflection;
using Common;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Shader
{
    public class ShaderProgramManager
    {
        private readonly List<ShaderProgram> shaderPrograms = new List<ShaderProgram>();

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

            shaderPrograms.Add(new ShaderProgram(vertexShader, fragmentShader));
        }

        public void Use(ShaderProgram targetShaderProgram)
        {
            foreach (var shaderProgram in shaderPrograms)
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