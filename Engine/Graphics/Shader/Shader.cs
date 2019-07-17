using System;
using System.IO;
using System.Reflection;
using System.Text;
using Engine.GlException;

using OpenTK.Graphics.OpenGL4;

namespace Engine.Shader
{
    public struct Shader
    {
        public readonly int GlShader;

        private string _fileName;
        
        public string ShaderSource
        {
            get
            {
                StringBuilder source = new StringBuilder();
                GL.GetShaderSource(GlShader, 1, out int length, source);
                return source.ToString();
            }
            set
            {
                GL.DeleteShader(GlShader);
                
                GL.ShaderSource(GlShader, value);
                
                Compile();
            }
        }
        
        public readonly ShaderType ShaderType;

        public Shader(string shaderSource, string fileName, ShaderType shaderType)
        {
            _fileName = fileName;
            
            ShaderType = shaderType;    
            
            GlShader = GL.CreateShader(shaderType);

            GL.ShaderSource(GlShader, shaderSource);
            
            Compile();
        }
        
        private void Compile()
        {
            GL.CompileShader(GlShader);
            
            GL.GetShader(GlShader, ShaderParameter.CompileStatus, out int glShaderCompileStatus);
            if (glShaderCompileStatus != 1)
            {
                throw new GlShaderCompileException(GlShader, _fileName);
            }
        }

        public static bool operator==(Shader shader, Shader targetShader)
        {
            return shader.GlShader == targetShader.GlShader;
        }
        
        public static bool operator!=(Shader shader, Shader targetShader)
        {
            return !(shader == targetShader);
        }
    }
}