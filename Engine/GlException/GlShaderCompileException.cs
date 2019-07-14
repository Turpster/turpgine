using OpenTK.Graphics.ES30;

namespace Engine.GlException
{
    public class GlShaderCompileException : GlException
    {
        public GlShaderCompileException(int glShader, string shaderSourceFile) : base("Compiling an OpenGL Shader: " + shaderSourceFile, 
            GL.GetShaderInfoLog(glShader)) { }
    }
}