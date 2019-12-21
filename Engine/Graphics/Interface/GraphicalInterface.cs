using Engine.Graphics.Model;
using Engine.Graphics.Scheduler;
using Logger;

namespace Engine.Graphics.Interface
{
    public abstract class GraphicalInterface : GlRenderHandler
    {
        private readonly GraphicalInterfaceManager _graphicalInterfaceManager;
        protected readonly ModelManager ModelManager;
        public readonly string Name;

        public bool Hidden = false;

        internal GraphicalInterface(GraphicalInterfaceManager graphicalInterfaceManager, string name) : base(graphicalInterfaceManager.GlMasterRenderHandler)
        {
            _graphicalInterfaceManager = graphicalInterfaceManager;
            Turpgine.Logger.Log(Level.Debug, "Creating Graphical Interface " + GetHashCode() + ".");
            Name = name;
            ModelManager = new ModelManager(graphicalInterfaceManager);
            _graphicalInterfaceManager._graphicalInterfaces.Add(Name, this);
        }

        public GraphicalInterface(Turpgine turpgine, string name) : this(turpgine.GraphicalInterfaceManager, name) { }

        ~GraphicalInterface()
        {
            Turpgine.Logger.Log(Level.Debug, "Deconstructing Graphical Interface " + GetHashCode() + ".");

            _graphicalInterfaceManager._graphicalInterfaces.Remove(Name);
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