using Engine.Graphics.Scheduler;

namespace Engine.Graphics.Interface
{
    public class Space : GraphicalInterface
    {
        public Space(GraphicalInterfaceManager graphicalInterfaceManager, string name) : base(graphicalInterfaceManager, name)
        {
        }

        public override GlCallResult _glInitialise()
        {
            throw new System.NotImplementedException();
        }

        public override GlCallResult _glDispose()
        {
            throw new System.NotImplementedException();
        }
    }
}