using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using Engine.Graphics.Interface;

namespace Engine.Graphics.Execution
{
    public abstract class GlObject : IDisposable
    {
        public bool GlInitialised = false;
        
        private static ObjectIDGenerator _idGenerator = new ObjectIDGenerator();
        
        public GlObject()
        {
            GlEventHandler.UninitGlObjects.Add(this, new GlAction(GlInitialise));
        }

        internal void GlInitialise()
        {
            GlEventHandler.GlCall(_glInitialise());
            GlInitialised = true;
        }

        internal void GlDispose()
        {
            _glDispose();
            GlInitialised = false;
        }

        protected abstract GlAction _glInitialise();
        protected abstract GlAction _glDispose();

        public long GetId()
        {
            return _idGenerator.GetId(this, out _);
        }

        public void Dispose() => new GlAction(GlDispose).Queue();
    }
}