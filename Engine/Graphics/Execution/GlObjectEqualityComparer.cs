using System;
using System.Collections;
using System.Collections.Generic;

namespace Engine.Graphics.Execution
{
    public class GlObjectEqualityComparer : IEqualityComparer<GlObject>
    {
        public bool Equals(GlObject x, GlObject y)
        {
            return x != null && y != null && x.GetId() == y.GetId();
        }

        public int GetHashCode(GlObject obj)
        {
            return obj.GetHashCode();
        }
    }
}