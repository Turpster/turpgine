using Engine.Graphics.Scheduler;

namespace Engine.Graphics
{
    public abstract class GlInitialisable
    {
        protected internal bool GlInitialised = false;

        internal void GlInitialise()
        {
            GlInitialised = true;
            _glInitialise();
        }

        internal void GlDispose()
        {
            GlInitialised = false;
            _glDispose();
        }

        public abstract GlCallResult _glInitialise();
        public abstract GlCallResult _glDispose();
    }
}