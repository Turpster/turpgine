using System;
using Engine.GlException;
using OpenTK.Graphics.OpenGL;

namespace Engine.Shader
{
    public class ShaderManager
    {
        private int GlProgram;
        
        public ShaderManager()
        {
            GlProgram = GL.CreateProgram();
        }

        public void Use()
        {
            GL.UseProgram(GlProgram);
        }
        
        public void AddShader(Shader shader)
        {
            if (ContainsShader(shader))
            {
                throw new GlShaderAttachException("Shader has already been attached.");
            }
            GL.AttachShader(GlProgram, shader.GlShader);
        }

        private void Unload()
        {
            GL.DeleteProgram(GlProgram);
            
        }

        private void Link()
        {
            GL.LinkProgram(GlProgram);
            GL.GetProgram(GlProgram, ProgramParameter.LinkStatus, out int linkStatus);
            if (linkStatus != 0)
            {
                throw new GlProgramLinkException(GL.GetProgramInfoLog(GlProgram));
            }
        }

        private void Validate()
        {
            GL.ValidateProgram(GlProgram);
            GL.GetProgram(GlProgram, ProgramParameter.ValidateStatus, out int validateStatus);
            if (validateStatus != 0)
            {
                throw new GlProgramValidateException(GL.GetProgramInfoLog(GlProgram));
            }
        }

        private void Load(int[] baseShaders=null)
        {
            GlProgram = GL.CreateProgram();

            if (baseShaders != null)
            {
                foreach (int shader in baseShaders)
                {
                    GL.AttachShader(GlProgram, shader);
                    GL.LinkProgram(GlProgram);
                    GL.ValidateProgram(GlProgram);
                }
            }
        }

        public void Reload()
        {
            int[] attachedShaders = GlAttachedShaders;
            Unload();
            Load(attachedShaders);
        }
        
        public void RemoveShader(Shader shader)
        {
            GL.DetachShader(GlProgram, shader.GlShader);
            Reload();
        }

        public bool ContainsShader(Shader shader)
        {
            foreach (var attachedShader in GlAttachedShaders)
            {
                if (attachedShader == shader.GlShader)
                {
                    return true;
                }
            }

            return false;
        }

        public int[] GlAttachedShaders
        {
            get
            {
                int[] currentAttachedShaders = new int[2];
                GL.GetAttachedShaders(GlProgram,0, out int shaderCount, currentAttachedShaders); // 2 might need to change
                return currentAttachedShaders;
            }
        }
    }
}