using System.Text;
using Engine.Graphics.GlException;
using Logger;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Shader
{
    public class Shader : GlObject
    {
        public readonly ShaderType ShaderType;

        private readonly string _fileName;
        public int GlShader;

        public string ShaderSource;

        public Shader(string shaderSource, string fileName, ShaderType shaderType)
        {
            _fileName = fileName;

            ShaderType = shaderType;

            ShaderSource = shaderSource;
        }

        public string GlShaderSource
        {
            get
            {
                Engine.Logger.Log(Level.Debug, "Getting Shader Source for shader " + GetHashCode() + ".");

                var source = new StringBuilder();
                GL.GetShaderSource(GlShader, 1, out _, source);
                return source.ToString();
            }
            set
            {
                GL.DeleteShader(GlShader);

                Engine.Logger.Log(Level.Debug, "Setting Shader Source for shader " + GetHashCode() + ".");
                GL.ShaderSource(GlShader, value);

                Compile();
            }
        }

        private void Compile()
        {
            Engine.Logger.Log(Level.Debug, "Compiling Shader Source for shader " + GetHashCode() + ".");

            GL.CompileShader(GlShader);

            GL.GetShader(GlShader, ShaderParameter.CompileStatus, out var glShaderCompileStatus);
            if (glShaderCompileStatus != 1) throw new GlShaderCompileException(GlShader, _fileName);
        }

        public static bool operator ==(Shader shader, Shader targetShader)
        {
            if (!ReferenceEquals(shader, null) && !ReferenceEquals(targetShader, null))
                if (shader.GlShader == targetShader.GlShader)
                    return true;

            return ReferenceEquals(shader, targetShader);
        }

        public static bool operator !=(Shader shader, Shader targetShader)
        {
            return !(shader == targetShader);
        }

        public bool Equals(Shader other)
        {
            return GlShader == other.GlShader;
            ;
        }

        public override bool Equals(object obj)
        {
            return obj is Shader other && Equals(other);
        }

        public override int GetHashCode()
        {
            unchecked
            {
                var hashCode = GlShader;
                hashCode = (hashCode * 397) ^ (_fileName != null ? _fileName.GetHashCode() : 0);
                hashCode = (hashCode * 397) ^ (int) ShaderType;
                return hashCode;
            }
        }

        protected internal override void GlInitialise()
        {
            GlShader = GL.CreateShader(ShaderType);

            GL.ShaderSource(GlShader, ShaderSource);

            Compile();
        }

        protected internal override void GlTerminate()
        {
            GL.DeleteShader(GlShader);
        }
    }
}