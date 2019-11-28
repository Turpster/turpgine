using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Execution.GlEvent
{
    public struct GlEventTextureFilter : IGlEvent
    {
        public readonly TextureMinFilter TextureMinFilter;
        public TextureMagFilter TextureMagFilter => (TextureMagFilter) TextureMinFilter;

        public GlEventTextureFilter(TextureMinFilter textureMinFilter)
        {
            TextureMinFilter = textureMinFilter;
        }

        public GlEventTextureFilter(TextureMagFilter textureMagFilter)
        {
            TextureMinFilter = (TextureMinFilter) textureMagFilter;
        }
    }
}