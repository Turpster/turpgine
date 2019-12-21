namespace Engine.Graphics.Model.Texture
{
    public class ImageTexture : Texture
    {
        public string Url;

        public ImageTexture(Mesh mesh, string url) : base(mesh)
        {
            Url = url;
        }
    }
}