using System;
using System.IO;
using OpenTK.Graphics.OpenGL4;

namespace Engine.Shader
{
    public class Shader
    {
        public readonly string ShaderFileLoc; 
        private int gl_shader;
        
        public Shader(string shaderFileLoc)
        {
            ShaderFileLoc = shaderFileLoc;

            gl_shader = GL.CreateProgram();
            
            string shaderSource = File.ReadAllText(shaderFileLoc);
            
            GL.ShaderSource(gl_shader, shaderSource);
            GL.CompileShader(gl_shader);
            
            GL.GetShader(gl_shader, ShaderParameter.CompileStatus, out int isCompiled);
            if (isCompiled == 0)
            {
                Console.WriteLine(GL.GetShaderInfoLog(gl_shader));
                throw new ShaderCompileException(gl_shader);
            }
        }

        public void Use()
        {
            GL.UseProgram(gl_shader);
        }
    }
}