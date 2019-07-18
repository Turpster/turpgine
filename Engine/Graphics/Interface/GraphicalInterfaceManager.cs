using System.Collections.Generic;

namespace Engine.Graphics.Interface
{
    public class GraphicalInterfaceManager : IRenderable
    {
        protected internal Dictionary<string, GraphicalInterface> _graphicalInterfaces = new Dictionary<string, GraphicalInterface>();
        
        // <Graphical Interface Name, Graphical Interface>
        public Dictionary<string, GraphicalInterface> GraphicalInterfaces => _graphicalInterfaces;
        
        public GraphicalInterfaceManager()
        {
            
        }

        public void Render()
        {
            var enumerator = _graphicalInterfaces.Values.GetEnumerator();

            do
            {
                var graphicalInterface = enumerator.Current;

                if (!graphicalInterface.Hidden)
                {
                    graphicalInterface.Render();
                }
            }
            while (enumerator.MoveNext());
        }
    }
}