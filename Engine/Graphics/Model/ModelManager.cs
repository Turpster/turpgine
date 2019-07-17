using System.Collections.Generic;

namespace Engine.Graphics.Model
{
    public class ModelManager : IRenderable
    {
        // <GameModel Hash, GameObject>
        // TODO Current implementation of GetHashCode is not completely unique.
        private Dictionary<int, Model> _gameModels { get; } = new Dictionary<int, Model>();

        public Dictionary<int, Model>.ValueCollection GameModels => _gameModels.Values;

        public void Render()
        
        {
            foreach (var gameObject in GameModels) gameObject.Render();
        }

        public void Add(Model model)
        {
            _gameModels.Add(model.GetHashCode(), model);
        }

        public void Remove(Model model)
        {
            _gameModels.Remove(model.GetHashCode());
        }
    }
}