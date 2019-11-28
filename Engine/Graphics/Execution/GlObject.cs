using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Graphics.Interface;

namespace Engine.Graphics.Execution
{
    public abstract class GlObject : IDisposable 
    {
        public bool GlInitialised = false;
        
        public GlObject()
        {
            GlEventHandler.GlActions.Enqueue(new GlAction(GlInitialise));
        }

        protected internal void GlInitialise()
        {
            GlInitialised = true;
            _glInitialise();
        }

        protected internal void GlDispose()
        {
            GlInitialised = false;
            _glDispose();
        }

        protected internal abstract void _glInitialise();
        protected internal abstract void _glDispose();

        public void Dispose() => new GlAction(GlDispose).Queue();
    }
}