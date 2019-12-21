using System.Collections.Generic;

namespace Engine.Graphics.Scheduler
{
    public class GlObjectEqualityComparer : EqualityComparer<GlObject>
    {
        public GlObjectEqualityComparer()
        {
            
        }
        
        public override bool Equals(GlObject x, GlObject y)
        {
            return x == y;
        }
        
        public override int GetHashCode(GlObject obj)
        {
            return obj.GetHashCode();
        }
    }
}