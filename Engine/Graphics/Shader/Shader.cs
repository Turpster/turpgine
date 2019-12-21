using System.Text;
using Engine.Graphics.GlException;
using Engine.Graphics.Scheduler;
using Logger;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Shader
{
    public class Shader : GlObject
    {
        protected ShaderProgramManager ShaderProgramManager;
        
        public readonly ShaderType ShaderType;

        private readonly string _fileName;
        public int GlShader;

        public string ShaderSource;

        public Shader(ShaderProgramManager shaderProgramManager, string shaderSource, string fileName, ShaderType shaderType) : base(shaderProgramManager)
        {
            ShaderProgramManager = shaderProgramManager;
            
            _fileName = fileName;

            ShaderType = shaderType;

            ShaderSource = shaderSource;
        }

        public string GlShaderSource
        {
            get
            {
                Turpgine.Logger.Log(Level.Debug, "Getting Shader Source for shader " + GetHashCode() + ".");
                
                GL.GetShaderSource(GlShader, 1, out _, out string source);
                return source;
            }
            set
            {
                GL.DeleteShader(GlShader);

                Turpgine.Logger.Log(Level.Debug, "Setting Shader Source for shader " + GetHashCode() + ".");
                GL.ShaderSource(GlShader, value);

                Compile();
            }
        }

        private void Compile()
        {
            Turpgine.Logger.Log(Level.Debug, "Compiling Shader Source for shader " + GetHashCode() + ".");

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


        public override GlCallResult _glInitialise()
        {
            return ShaderProgramManager.GlCall(() =>
            {
                GlShader = GL.CreateShader(ShaderType);

                GL.ShaderSource(GlShader, ShaderSource);

                Compile();
            });
        }


        public override GlCallResult _glDispose()
        {
            return ShaderProgramManager.GlCall(() =>
            {
                GL.DeleteShader(GlShader);
            });
        }
    }
}