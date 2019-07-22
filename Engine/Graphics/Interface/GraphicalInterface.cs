using Engine.Graphics.Model;
using Logger;
using OpenTK;
using OpenTK.Graphics.OpenGL;

namespace Engine.Graphics.Interface
{
    public abstract class GraphicalInterface : GlObject, IRenderable 
    {
        protected readonly ModelManager ModelManager = new ModelManager();

        private readonly GraphicalManager _graphicalManager;
        public readonly string Name;

        public bool Hidden = false;

        public GraphicalInterface(GraphicalManager graphicalManager, string name)
        {
            Engine.Logger.Log(Level.Debug, "Creating Graphical Interface " + this.GetHashCode() + ".");
            
            _graphicalManager = graphicalManager;
            Name = name;

            _graphicalManager._graphicalInterfaces.Add(Name, this);
        }

        ~GraphicalInterface()
        {
            Engine.Logger.Log(Level.Debug, "Deconstructing Graphical Interface " + this.GetHashCode() + ".");
            
            _graphicalManager._graphicalInterfaces.Remove(Name);
        }

        protected internal override void GlInitialise()
        {
            ModelManager.GlInitialise();
        }
        
        protected internal override void GlTerminate()
        {
            ModelManager.GlTerminate();
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