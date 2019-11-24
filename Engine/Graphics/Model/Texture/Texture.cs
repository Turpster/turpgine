using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp;

namespace Engine.Graphics.Model.Texture
{
    public abstract class Texture : GlObject
    {
        private Image Image { get; }

        public Texture(string url)
        {
            Image = Image.Load<Rgba32>(url);
        }

        public Texture(Image image)
        {
            Image = image;
        }
    }
}