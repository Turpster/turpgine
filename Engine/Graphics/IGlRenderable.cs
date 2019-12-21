using System;
using Engine.Graphics.Scheduler;

namespace Engine.Graphics
{
    public interface IGlRenderable
    {
        GlCallResult GlRender();
    }
}