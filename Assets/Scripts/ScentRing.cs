
using UnityEngine;

namespace LastKnownPosition
{
    public class ScentRing : MonoBehaviour, IRing
    {
        public float Radius { get; private set; }
        public Vector2 Center { get; private set; }
        public float Weight { get; private set; }
        public float? WeightedPercentage { get; set; }
        public Vector2 ChildCenter { get; }
        
        public ScentRing()
        {
            
        }

        public void Initialize(Vector2 center, float radius, float weight)
        {
            Center = center;
            Radius = radius;
            Weight = weight;
        }
    }
}