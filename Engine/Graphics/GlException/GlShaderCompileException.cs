using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.GlException
{
    public class GlShaderCompileException : GlException
    {
        public GlShaderCompileException(int glShader, string shaderSourceFile) : base(
            "Compiling an OpenGL Shader: " + shaderSourceFile,
            GL.GetShaderInfoLog(glShader))
        {
        }
    }
}