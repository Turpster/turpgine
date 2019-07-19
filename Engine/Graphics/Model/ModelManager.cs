using System.Collections.Generic;
using Logger;

namespace Engine.Graphics.Model
{
    public class ModelManager : IRenderable
    {
        // <GameModel Hash, GameObject>
        // TODO Current implementation of GetHashCode is not completely unique.
        private Dictionary<int, Model> _gameModels { get; } = new Dictionary<int, Model>();

        public Dictionary<int, Model>.ValueCollection GameModels => _gameModels.Values;

        public ModelManager()
        {
            Engine.Logger.Log(Level.Debug, "Creating Model Manager " + this.GetHashCode() + ".");
        }
        
        public void Render()
        {
            foreach (var gameObject in GameModels) gameObject.Render();
        }

        public void GlInit()
        {
            foreach (var gameObject in GameModels) gameObject.GlInit();
        }

        protected internal void Add(Model model)
        {
            _gameModels.Add(model.GetHashCode(), model);
        }

        protected internal void Remove(Model model)
        {
            _gameModels.Remove(model.GetHashCode());
        }
    }
}