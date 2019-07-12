using System.Text;
using Engine.GlException;
using OpenTK.Graphics.OpenGL;
using ErrorCode = OpenTK.Graphics.OpenGL4.ErrorCode;

namespace Engine.Shader
{
    public class ShaderManager
    {
        private int GlProgram;

        public ShaderManager(Shader? vertexShader=null, Shader? fragmentShader=null)
        {
            if (vertexShader.HasValue)
            {
                VertexShader = vertexShader;
            }

            if (fragmentShader.HasValue)
            {
                FragmentShader = fragmentShader;
            }

            Load();
        }

        public Shader?[] Shaders { get; } = new Shader?[ShaderIndex.Num];

        public Shader? VertexShader
        {
            get => Shaders[ShaderIndex.Vertex.Value];
            set
            {
                var vertIndex = ShaderIndex.Vertex.Value;
                
                Shaders[vertIndex] = value;
                
                Reload();
            }
        }

        public Shader? FragmentShader
        {
            get => Shaders[ShaderIndex.Fragment.Value];
            set
            {
                var fragIndex = ShaderIndex.Fragment.Value;
                
                Shaders[fragIndex] = value;
                
                Reload();
            }
        }

        ~ShaderManager()
        {
            Unload();
        }


        public void Use()
        {
            GL.UseProgram(GlProgram);
        }

        public bool ContainsShader(Shader glShader)
        {
            foreach (var shader in Shaders)
            {
                if (shader == null) continue;

                if (shader.Value.GlShader == glShader.GlShader) return true;
            }

            return false;
        }

        private void Unload()
        {
            GL.DeleteProgram(GlProgram);
        }

        private void Link()
        {
            GL.LinkProgram(GlProgram);
            GL.GetProgram(GlProgram, ProgramParameter.LinkStatus, out var linkStatus);
            if (linkStatus != 0) throw new GlProgramLinkException(GL.GetProgramInfoLog(GlProgram));
        }

        private void Validate()
        {
            GL.ValidateProgram(GlProgram);
            GL.GetProgram(GlProgram, ProgramParameter.ValidateStatus, out var validateStatus);
            if (validateStatus != 0) throw new GlProgramValidateException(GL.GetProgramInfoLog(GlProgram));
        }

        private void Load()
        {
            GlProgram = GL.CreateProgram();
            
            foreach (var shader in Shaders)
                if (shader.HasValue)
                    GL.AttachShader(GlProgram, shader.Value.GlShader);
            
            Link();
            Validate();
        }

        public void Reload()
        {
            Unload();
            Load();
        }

        private class ShaderIndex
        {
            public static readonly ShaderIndex
                Vertex = new ShaderIndex(0, OpenTK.Graphics.OpenGL.ShaderType.VertexShader),
                Fragment = new ShaderIndex(1, OpenTK.Graphics.OpenGL.ShaderType.FragmentShader);

            public static int Num;
            public readonly ShaderType? ShaderType;

            public readonly int Value;

            public ShaderIndex(int value, ShaderType shaderType)
            {
                Value = value;
                ShaderType = shaderType;

                Num++;
            }
        }
    }
}