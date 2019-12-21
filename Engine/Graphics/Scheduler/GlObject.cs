using System;
using System.Runtime.Serialization;
using OpenTK.Input;

namespace Engine.Graphics.Scheduler
{
    public abstract class GlObject : GlInitialisable, IDisposable
    {
        protected readonly GlRenderHandler GlRenderHandler;
        private static ObjectIDGenerator _idGenerator = new ObjectIDGenerator();

        protected internal GlObject(GlRenderHandler glRenderHandler)
        {
            GlRenderHandler = glRenderHandler;
            GlRenderHandler.AddUnitGlObject(this, new GlAction(GlInitialise, glRenderHandler, false));
        }

        public long GetId()
        {
            return _idGenerator.GetId(this, out _);
        }

        public void Dispose() => new GlAction(GlDispose, GlRenderHandler, false).Queue();

        public static bool operator ==(GlObject glObject, GlObject targetGlObject)
        {
            if (glObject is null && targetGlObject is null)
            {
                return true;
            }
            return !(glObject is null) && !(targetGlObject is null) && glObject.GetId() == targetGlObject.GetId();
        }

        public static bool operator!=(GlObject glObject, GlObject targetGlObject)
        {
            return !(glObject == targetGlObject);
        }
        
        protected bool Equals(GlObject other)
        {
            return GetId() == other.GetId();
        }
    }
}