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
            _glInitialise();
            GlInitialised = true;
        }

        protected internal void GlDispose()
        {
            _glDispose();
            GlInitialised = false;
        }

        protected internal abstract void _glInitialise();
        protected internal abstract void _glDispose();

        public void Dispose() => new GlAction(GlDispose).Queue();
    }
}