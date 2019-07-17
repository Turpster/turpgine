using System;
using System.Collections.Generic;
using engine;

namespace Engine.GameObject
{
    public class ModelManager : IRenderable
    {
        // <GameModel Hash, GameObject>
        private Dictionary<int, Model> _gameObjects { get; } = new Dictionary<int, Model>();

        Dictionary<int, Model>.ValueCollection GameObjects => _gameObjects.Values;
        
        public void Render()
        {
            foreach (var gameObject in GameObjects)
            {
                gameObject.Render();
            }
        }

        public void Add(Model model)
        {
            _gameObjects.Add(model.GetHashCode(), model);
        }

        public void Remove(Model model)
        {
            _gameObjects.Remove(model.GetHashCode());
        }
    }
}