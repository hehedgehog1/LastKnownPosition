
using UnityEngine;

namespace LastKnownPosition
{
    public class ScentRing : MonoBehaviour, IRing
    {
        public float Radius { get; }
        public Vector2 Center { get; }
        public float Weight { get; }
        public float? WeightedPercentage { get; set; }
        public Vector2 ChildCenter { get; }
        
        public ScentRing()
        {
            Center = new Vector2(5, -3);
            Radius = 0.5f;
            Weight = 2;
        }
    }
}