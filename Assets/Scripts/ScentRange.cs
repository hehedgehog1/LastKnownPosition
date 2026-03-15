using UnityEngine;

namespace LastKnownPosition
{
    public class ScentRange
    {
        public Vector2 PointA { get; }
        public Vector2 PointB { get; }

        public ScentRange(Vector2 pointA, Vector2 pointB)
        {
            PointA = pointA;
            PointB = pointB;
        }
    }
}