using engine;
using Engine.GameObject._3D;

namespace Engine.GraphicalInterface
{
    public class Space : GraphicalInterface, IRenderable
    {   
        public Space()
        {
            
        }
        
        public void Add(Model3D obj)
        {
            ModelManager.Add(obj);
        }
    }
}