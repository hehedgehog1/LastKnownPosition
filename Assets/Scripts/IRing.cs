

using UnityEngine;

namespace LastKnownPosition
{
    public interface IRing
    {
        public float Radius { get; }
        public Vector2 Center { get; }
    }
}