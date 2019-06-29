using OpenTK.Graphics.ES30;

namespace Engine.GlException
{
    public class GlShaderCompileException : GlException
    {
        public override string Message { get; }

        public GlShaderCompileException(int glShader) : base("Compiling an OpenGL Shader", 
            GL.GetShaderInfoLog(glShader)) { }
    }
}