using System.Collections.Generic;
using Logger;

namespace Engine.Graphics.Model
{
    public class ModelManager : GlObject, IRenderable
    {
        public ModelManager()
        {
            Engine.Logger.Log(Level.Debug, "Creating Model Manager " + GetHashCode() + ".");
        }

        // <GameModel Hash, GameObject>
        // TODO Current implementation of GetHashCode is not completely unique.
        private Dictionary<int, Model> _gameModels { get; } = new Dictionary<int, Model>();

        public Dictionary<int, Model>.ValueCollection GameModels => _gameModels.Values;

        public void Render()
        {
            foreach (var gameObject in GameModels) gameObject.Render();
        }

        protected internal override void GlInitialise()
        {
            foreach (var gameObject in GameModels) gameObject.GlInitialise();
        }

        protected internal override void GlDispose()
        {
            foreach (var gameObject in GameModels) gameObject.GlDispose();
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