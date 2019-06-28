using System;
using engine;
using OpenTK.Graphics.ES30;

namespace Engine.Shader
{
    public class ShaderCompileException : GlException
    {
        public override string Message { get; }

        public ShaderCompileException(int gl_shader) : base("Compiling an OpenGL Shader", 
            GL.GetShaderInfoLog(gl_shader)) { }
    }
}