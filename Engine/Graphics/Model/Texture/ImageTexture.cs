using SixLabors.ImageSharp;

namespace Engine.Graphics.Model.Texture
{
    public class ImageTexture : Texture
    {
        public ImageTexture(string url) : base(url)
        {
            
        }

        public ImageTexture(Image image) : base(image)
        {
            
        }

        protected internal override void GlInitialise()
        {
            throw new System.NotImplementedException();
        }

        protected internal override void GlDispose()
        {
            throw new System.NotImplementedException();
        }
    }
}