using Engine.Graphics.Execution;
using Engine.Graphics.Execution.GlEvent;
using OpenTK.Graphics.OpenGL;
using SixLabors.ImageSharp.PixelFormats;

namespace Engine.Graphics.Model.Texture
{
    public abstract class Texture : GlObject
    {
        protected internal uint GlTexture;

        public GlEventTextureFilter GlTextureMinFilter
        {
            get
            {
                return GlEventHandler.GlCallSync(() =>
                {
                    GL.BindTexture(TextureTarget.Texture2D, GlTexture);
                    GL.GetTexParameterI(TextureTarget.Texture2D, GetTextureParameter.TextureMinFilter,
                        out int textureFilter);
                    return new GlEventTextureFilter((TextureMinFilter) textureFilter);
                });
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
                return GlEventHandler.GlCallSync(() =>
                {
                    GL.BindTexture(TextureTarget.Texture2D, GlTexture);
                    GL.GetTexParameterI(TextureTarget.Texture2D, GetTextureParameter.TextureMagFilter,
                        out int textureFilter);
                    return new GlEventTextureFilter((TextureMagFilter) textureFilter);
                });
            }
            set
            {
                GlEventHandler.GlCall(() =>
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

        protected internal override GlAction _glInitialise()
        {
            return new GlAction(() =>
            {
                GlWrapModeX = TextureWrapMode.Repeat;
                GlWrapModeY = TextureWrapMode.Repeat;

                GlTextureMinFilter = new GlEventTextureFilter(TextureMagFilter.Linear);
                GlTextureMagFilter = new GlEventTextureFilter(TextureMagFilter.Linear);
            });
        }

        protected internal override GlAction _glDispose()
        {
            throw new System.NotImplementedException();
        }
    }
}