using Engine.Graphics.Model._3D;

namespace Engine.Graphics.Interface
{
    public class Space : GraphicalInterface, IRenderable
    {
        public override void Render()
        {
        }

        public void Add(Model3D obj)
        {
            ModelManager.Add(obj);
        }

        public void Remove(Model3D obj)
        {
            ModelManager.Add(obj);
        }
    }
}