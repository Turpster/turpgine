using System;
using Engine.Graphics.Execution;
using Engine.Graphics.GlException;
using Logger;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Shader
{
    public class ShaderProgram : GlObject
    {
        private int GlProgram;

        public ShaderProgram(Shader vertexShader, Shader fragmentShader)
        {
            VertexShader = vertexShader;
            FragmentShader = fragmentShader;
        }

        public Shader[] Shaders { get; } = new Shader[ShaderIndex.Num];

        protected internal Shader VertexShader
        {
            get => Shaders[ShaderIndex.Vertex.Value];
            set
            {
                Engine.Logger.Log(Level.Debug, "Using Vertex Shader " + value.GetHashCode() + ".");

                var vertIndex = ShaderIndex.Vertex.Value;

                Shaders[vertIndex] = value;
            }
        }

        protected internal Shader FragmentShader
        {
            get => Shaders[ShaderIndex.Fragment.Value];
            set
            {
                Engine.Logger.Log(Level.Debug, "Using Fragment Shader " + value.GetHashCode() + ".");

                var fragIndex = ShaderIndex.Fragment.Value;

                Shaders[fragIndex] = value;
            }
        }

        ~ShaderProgram()
        {
            // TODO This could possibly be executed in wrong thread.

            _glDispose();
        }


        protected internal void Use()
        {
            GL.UseProgram(GlProgram);
        }

        public bool ContainsShader(Shader glShader)
        {
            foreach (var shader in Shaders)
                if (shader == glShader)
                    return true;

            return false;
        }

        private void GlUnload()
        {
            Engine.Logger.Log(Level.Debug, "Unloading ShaderProgram " + GetHashCode() + ".");

            GL.DeleteProgram(GlProgram);
        }

        private void Link()
        {
            Engine.Logger.Log(Level.Debug, "Linking ShaderProgram " + GetHashCode() + ".");

            GL.LinkProgram(GlProgram);
            GL.GetProgram(GlProgram, GetProgramParameterName.LinkStatus, out var linkStatus);
            if (linkStatus != 1) throw new GlProgramLinkException(GL.GetProgramInfoLog(GlProgram));
        }

        private void Validate()
        {
            Engine.Logger.Log(Level.Debug, "Validating ShaderProgram " + GetHashCode() + ".");

            GL.ValidateProgram(GlProgram);
            GL.GetProgram(GlProgram, GetProgramParameterName.ValidateStatus, out var validateStatus);
            if (validateStatus != 1) throw new GlProgramValidateException(GL.GetProgramInfoLog(GlProgram));
        }

        private void GlLoad()
        {
            Engine.Logger.Log(Level.Debug, "Loading ShaderProgram " + GetHashCode() + ".");

            GlProgram = GL.CreateProgram();

            foreach (var shader in Shaders)
                if (shader != null)
                    GL.AttachShader(GlProgram, shader.GlShader);

            Link();
            Validate();
        }

        public void GlReload()
        {
            GlUnload();
            GlLoad();
        }

        protected internal override void _glInitialise()
        {
            foreach (var shader in Shaders) shader._glInitialise();

            GlLoad();

            foreach (var shader in Shaders) shader._glDispose();
        }

        protected internal override void _glDispose()
        {
            throw new NotImplementedException();
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