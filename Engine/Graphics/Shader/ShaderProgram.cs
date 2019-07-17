using System.Reflection;
using System.Text;
using Common;
using Engine.GlException;
using OpenTK.Graphics.OpenGL;

namespace Engine.Shader
{
    public class ShaderProgram
    {
        private int GlProgram;

        public ShaderProgram(Shader? vertexShader=null, Shader? fragmentShader=null)
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

        public ShaderProgram()
        {
            Assembly assembly = Assembly.GetExecutingAssembly();

            VertexShader =
                new Shader(
                    StreamUtil.ReadStringStream(
                        assembly.GetManifestResourceStream("Engine.Shader.GLSL.vertex-shader.vert")),
                    "vertex-shader.vert", OpenTK.Graphics.OpenGL4.ShaderType.VertexShader);
            FragmentShader =
                new Shader(
                    StreamUtil.ReadStringStream(
                        assembly.GetManifestResourceStream("Engine.Shader.GLSL.fragment-shader.frag")),
                    "fragment-shader.frag", OpenTK.Graphics.OpenGL4.ShaderType.FragmentShader);
            
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

        ~ShaderProgram()
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
            GL.GetProgram(GlProgram, GetProgramParameterName.LinkStatus, out var linkStatus);
            if (linkStatus != 1) throw new GlProgramLinkException(GL.GetProgramInfoLog(GlProgram));
        }

        private void Validate()
        {
            GL.ValidateProgram(GlProgram);
            GL.GetProgram(GlProgram, GetProgramParameterName.ValidateStatus, out var validateStatus);
            if (validateStatus != 1) throw new GlProgramValidateException(GL.GetProgramInfoLog(GlProgram));
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