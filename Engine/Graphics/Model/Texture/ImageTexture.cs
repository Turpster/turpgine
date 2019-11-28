using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Model.Texture
{
    public class ImageTexture : Texture
    {
        public string Url;

        public ImageTexture(string url)
        {
            Url = url;
        }
    }
}