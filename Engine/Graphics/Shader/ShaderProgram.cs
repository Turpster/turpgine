using System.Reflection;
using Common;
using Engine.Graphics.GlException;
using Logger;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Shader
{
    public class ShaderProgram
    {
        private int GlProgram;
        
        public ShaderProgram(Shader vertexShader, Shader fragmentShader)
        {
            VertexShader = vertexShader;
            FragmentShader = fragmentShader;

            Load();
        }

        public Shader?[] Shaders { get; } = new Shader?[ShaderIndex.Num];

        public Shader? VertexShader
        {
            get => Shaders[ShaderIndex.Vertex.Value];
            set
            {
                Engine.Logger.Log(Level.Debug, "Using Vertex Shader " + value.GetHashCode() + ".");
                
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
                Engine.Logger.Log(Level.Debug, "Using Fragment Shader " + value.GetHashCode() + ".");
                
                var fragIndex = ShaderIndex.Fragment.Value;

                Shaders[fragIndex] = value;

                Reload();
            }
        }

        ~ShaderProgram()
        {
            Unload();
        }


        protected internal void Use()
        {
            GL.UseProgram(GlProgram);
        }

        public bool ContainsShader(Shader glShader)
        {
            foreach (var shader in Shaders)
            {
                if (shader == null) continue;
                
                if (shader.Value == glShader) return true; 
            }

            return false;
        }

        private void Unload()
        {
            Engine.Logger.Log(Level.Debug, "Unloading ShaderProgram " + this.GetHashCode() + ".");
            
            GL.DeleteProgram(GlProgram);
        }

        private void Link()
        {
            Engine.Logger.Log(Level.Debug, "Linking ShaderProgram " + this.GetHashCode() + ".");
            
            GL.LinkProgram(GlProgram);
            GL.GetProgram(GlProgram, GetProgramParameterName.LinkStatus, out var linkStatus);
            if (linkStatus != 1) throw new GlProgramLinkException(GL.GetProgramInfoLog(GlProgram));
        }

        private void Validate()
        {
            Engine.Logger.Log(Level.Debug, "Validating ShaderProgram " + this.GetHashCode() + ".");

            GL.ValidateProgram(GlProgram);
            GL.GetProgram(GlProgram, GetProgramParameterName.ValidateStatus, out var validateStatus);
            if (validateStatus != 1) throw new GlProgramValidateException(GL.GetProgramInfoLog(GlProgram));
        }

        private void Load()
        {
            Engine.Logger.Log(Level.Debug, "Loading ShaderProgram " + this.GetHashCode() + ".");
            
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