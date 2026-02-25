using System.Collections.Generic;
using UnityEngine;

namespace LastKnownPosition
{
    public class DogRing : IRing
    {
        public float Radius { get; }
        public Vector2 Center { get; }
        public IList<Collision> Collisions { get; }

        public void DisplayDirectionRange(Vector2 pointA, Vector2 pointB)
        {
            
        }
    }
}