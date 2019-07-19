using System;
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

        // TODO Create properties to pickup current shader through OpenGL.
        private static int _currentShaderHash = 0;
        
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

        public ShaderProgramManager(ShaderProgram shaderProgram)
        {
            Add(shaderProgram);
        }

        public void Use(ShaderProgram targetShaderProgram)
        {
            if (!_shaderPrograms.ContainsKey(targetShaderProgram.GetHashCode()))
            {
                throw new ArgumentException("Shader Program has not been added to Hash Dictionary.");
            }

            targetShaderProgram.Use();
            _currentShaderHash = targetShaderProgram.GetHashCode();
        }

        // TODO Ensure ShaderProgram is not bound to any other manager
        public void Add(ShaderProgram shaderProgram, bool use=false)
        {
            _shaderPrograms.Add(shaderProgram.GetHashCode(), shaderProgram);
            if (use) Use(shaderProgram);
        }

        public void Remove(ShaderProgram shaderProgram) => _shaderPrograms.Remove(shaderProgram.GetHashCode());
    }
}