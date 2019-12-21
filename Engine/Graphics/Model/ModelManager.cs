using System;
using System.Collections.Generic;
using Engine.Graphics.Interface;
using Engine.Graphics.Scheduler;
using Logger;
using OpenTK.Graphics.OpenGL;


namespace Engine.Graphics.Model
{
    public class ModelManager : GlRenderHandler
    {
        public ModelManager(GraphicalInterfaceManager glGraphicalInterfaceManager) 
            : base(glGraphicalInterfaceManager)
        {
            Turpgine.Logger.Log(Level.Debug, "Creating Model Manager " + GetHashCode() + ".");
        }

        // <GameModel Hash, GameModel>
        // TODO Current implementation of GetHashCode is not completely unique.
        private Dictionary<int, Model> _gameModels { get; } = new Dictionary<int, Model>();

        public Dictionary<int, Model>.ValueCollection GameModels => _gameModels.Values;

        protected internal void Add(Model model)
        {
            _gameModels.Add(model.GetHashCode(), model);
        }

        protected internal void Remove(Model model)
        {
            _gameModels.Remove(model.GetHashCode());
        }

        public override GlCallResult _glInitialise()
        {
            return GlCall(() =>
            {
                GL.ClearColor(0.05f, 0.15f, 0.3f, 1.0f);
            });
        }

        public override GlCallResult _glDispose()
        {
            throw new NotImplementedException();
        }
    }
}