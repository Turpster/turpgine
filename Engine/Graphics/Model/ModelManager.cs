using System.Collections.Generic;

namespace Engine.Graphics.Model
{
    public class ModelManager : IRenderable
    {
        // TODO Current implementation of GetHashCode is not completely unique.
        // Possible fix is to ensure GetHashCode is unique for Model's.
        private HashSet<Model> _gameModels { get; } = new HashSet<Model>();

        public HashSet<Model> GameModels => _gameModels;

        public void Render()
        {
            foreach (var gameObject in GameModels) gameObject.Render();
        }

        public void Add(Model model)
        {
            _gameModels.Add(model);
        }

        public void Remove(Model model)
        {
            _gameModels.Remove(model);
        }
    }
}