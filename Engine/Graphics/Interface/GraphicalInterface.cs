using Engine.Graphics.Model;
using Logger;

namespace Engine.Graphics.Interface
{
    public abstract class GraphicalInterface : GlObject, IRenderable
    {
        private readonly GraphicalManager _graphicalManager;
        protected readonly ModelManager ModelManager = new ModelManager();
        public readonly string Name;

        public bool Hidden = false;

        public GraphicalInterface(GraphicalManager graphicalManager, string name)
        {
            Engine.Logger.Log(Level.Debug, "Creating Graphical Interface " + GetHashCode() + ".");

            _graphicalManager = graphicalManager;
            Name = name;

            _graphicalManager._graphicalInterfaces.Add(Name, this);
        }

        public void Render()
        {
            foreach (var model in ModelManager.GameModels) model.Render();
        }

        ~GraphicalInterface()
        {
            Engine.Logger.Log(Level.Debug, "Deconstructing Graphical Interface " + GetHashCode() + ".");

            _graphicalManager._graphicalInterfaces.Remove(Name);
        }

        protected internal override void GlInitialise()
        {
            ModelManager.GlInitialise();
        }

        protected internal override void GlDispose()
        {
            ModelManager.GlDispose();
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