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

        // <GameModel Hash, GameModel>
        // TODO Current implementation of GetHashCode is not completely unique.
        private Dictionary<int, Model> _gameModels { get; } = new Dictionary<int, Model>();

        public Dictionary<int, Model>.ValueCollection GameModels => _gameModels.Values;

        public void Render()
        {
            foreach (var gameModel in GameModels) gameModel.Render();
        }

        protected internal override void GlInitialise()
        {
            foreach (var gameModel in GameModels) gameModel.GlInitialise();
        }

        protected internal override void GlDispose()
        {
            foreach (var gameModel in GameModels) gameModel.GlDispose();
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