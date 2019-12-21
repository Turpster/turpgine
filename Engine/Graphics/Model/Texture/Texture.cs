using System;
using Engine.Graphics.Scheduler;
using Engine.Graphics.Scheduler.GlEvent;
using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp.PixelFormats;

namespace Engine.Graphics.Model.Texture
{
    public abstract class Texture : GlObject
    {
        protected internal uint GlTexture;
        
        protected Texture(Mesh mesh) : base(mesh.Model._modelManager)
        {
            
        }

        public GlEventTextureFilter GlTextureMinFilter
        {
            get
            {
                return GlRenderHandler.GlCallSync(() =>
                {
                    GL.BindTexture(TextureTarget.Texture2D, GlTexture);
                    GL.GetTexParameterI(TextureTarget.Texture2D, GetTextureParameter.TextureMinFilter,
                        out int textureFilter);
                    return new GlEventTextureFilter((TextureMinFilter) textureFilter);
                })._value;
            }
            set
            {
                GL.BindTexture(TextureTarget.Texture2D, GlTexture);
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, new [] {(int) value.TextureMinFilter});
            }
        }

        public GlEventTextureFilter GlTextureMagFilter
        {
            get
            {
                return GlRenderHandler.GlCallSync(() =>
                {
                    GL.BindTexture(TextureTarget.Texture2D, GlTexture);
                    GL.GetTexParameterI(TextureTarget.Texture2D, GetTextureParameter.TextureMagFilter,
                        out int textureFilter);
                    return new GlEventTextureFilter((TextureMagFilter) textureFilter);
                })._value;
            }
            set
            {
                GlRenderHandler.GlCall(() =>
                {
                    GL.BindTexture(TextureTarget.Texture2D, GlTexture);
                    GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter,
                        new[] {(int) value.TextureMagFilter});
                });
            }
        }

        public TextureWrapMode GlWrapModeX
        {
            set
            {
                GL.BindTexture(TextureTarget.Texture2D, GlTexture);
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, new [] { (int) value });
            }
            get
            {
                GL.BindTexture(TextureTarget.Texture2D, GlTexture);
                GL.GetTexParameterI(TextureTarget.Texture2D, GetTextureParameter.TextureWrapS, out int wrapMode);
                return (TextureWrapMode) wrapMode;
            }
        }
        
        public TextureWrapMode GlWrapModeY
        {
            set
            {
                GL.BindTexture(TextureTarget.Texture2D, GlTexture);
                GL.TexParameterI(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, new [] { (int) value });
            }
            get
            {
                GL.BindTexture(TextureTarget.Texture2D, GlTexture);
                GL.GetTexParameterI(TextureTarget.Texture2D, GetTextureParameter.TextureWrapT, out int warpMode);
                return (TextureWrapMode) warpMode;
            }
        }

        public Rgba32 GlBorderColor
        {
            set
            {
                GL.BindTexture(TextureTarget.Texture2D, GlTexture);
                float[] borderColor = {value.R, value.G, value.B, value.A};
                GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureBorderColor, borderColor);
            }
            get
            {
                GL.BindTexture(TextureTarget.Texture2D, GlTexture);
                float[] borderColor = new float[4];
                GL.GetTexParameter(TextureTarget.Texture2D, GetTextureParameter.TextureBorderColor, borderColor);
                return new Rgba32(borderColor[0], borderColor[1], borderColor[2], borderColor[3]);
            }
        }

        public override GlCallResult _glInitialise()
        {
            return GlRenderHandler.GlCall(() =>
            {
                GlWrapModeX = TextureWrapMode.Repeat;
                GlWrapModeY = TextureWrapMode.Repeat;

                GlTextureMinFilter = new GlEventTextureFilter(TextureMagFilter.Linear);
                GlTextureMagFilter = new GlEventTextureFilter(TextureMagFilter.Linear);
            });
        }

        public override GlCallResult _glDispose()
        {
            return GlRenderHandler.GlCall(() => throw new NotImplementedException());
        }
    }
}