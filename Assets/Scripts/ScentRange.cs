using System.Collections.Generic;
using UnityEngine;

namespace LastKnownPosition
{
    public class ScentRange
    {
        public IList<Vector2> Points { get; } = new List<Vector2>();

        public Vector2 PointA { get; }
        public Vector2 Point1 { get; }
        public Vector2 Point2 { get; }
        public Vector2 PointB { get; }

        public ScentRange()
        {
            
        }
        
        public ScentRange(Vector2 pointA, Vector2 pointB, Vector2 point1 = default, Vector2 point2 = default)
        {
            PointA = pointA;
            PointB = pointB;
            Point1 = point1;
            Point2 = point2;
        }
    }
}