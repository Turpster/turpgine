using Engine.Graphics.Model;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Interface
{
    public abstract class GraphicalInterface : IRenderable
    {
        protected readonly ModelManager ModelManager = new ModelManager();

        private readonly GraphicalInterfaceManager _graphicalInterfaceManager;
        public readonly string Name;

        public bool Hidden = false;

        public GraphicalInterface(GraphicalInterfaceManager graphicalInterfaceManager, string name)
        {
            _graphicalInterfaceManager = graphicalInterfaceManager;
            Name = name;

            _graphicalInterfaceManager._graphicalInterfaces.Add(Name, this);

        }

        ~GraphicalInterface()
        {
            _graphicalInterfaceManager._graphicalInterfaces.Remove(Name);
        }

        public void Render()
        {
            foreach (var model in ModelManager.GameModels)
            {
                model.Render();
            }
        }
        
        public void Add(Model.Model obj)
        {
            ModelManager.Add(obj);
        }

        public void Remove(Model.Model obj)
        {
            ModelManager.Add(obj);
        }
    }
}