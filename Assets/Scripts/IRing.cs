

using UnityEngine;

namespace LastKnownPosition
{
    public interface IRing
    {
        public double Radius { get; }
        public Vector2 Center { get; }
    }
}