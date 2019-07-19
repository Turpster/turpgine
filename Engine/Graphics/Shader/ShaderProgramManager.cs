using System;
using System.Collections.Generic;
using System.Reflection;
using Common;
using Logger;
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

            string vertexShaderResource = "Engine.Graphics.Shader.GLSL.vertex-shader.vert";            
            string fragmentShaderResource = "Engine.Graphics.Shader.GLSL.fragment-shader.frag";

            Engine.Logger.Log(Level.Debug, "Loading resource '" + vertexShaderResource + "'.");
            Shader vertexShader =
                new Shader(
                    StreamUtil.ReadStringStream(
                        assembly.GetManifestResourceStream(vertexShaderResource)),
                    "vertex-shader.vert", ShaderType.VertexShader);
            
            Engine.Logger.Log(Level.Debug, "Loading resource '" + fragmentShaderResource + "'.");
            Shader fragmentShader =
                new Shader(
                    StreamUtil.ReadStringStream(
                        assembly.GetManifestResourceStream(fragmentShaderResource)),
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
            Engine.Logger.Log(Level.Debug, "Using ShaderProgram " + targetShaderProgram.GetHashCode() + ".");
            
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
            Engine.Logger.Log(Level.Debug, "Adding ShaderProgram " + shaderProgram.GetHashCode() + ".");
            _shaderPrograms.Add(shaderProgram.GetHashCode(), shaderProgram);
            
            if (use) Use(shaderProgram);
        }

        public void Remove(ShaderProgram shaderProgram)
        {
            Engine.Logger.Log(Level.Debug, "Removing ShaderProgram " + shaderProgram.GetHashCode() + ".");
            _shaderPrograms.Remove(shaderProgram.GetHashCode());
        }
    }
}