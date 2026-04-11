
using UnityEngine;

namespace LastKnownPosition
{
    public class ScentRing : MonoBehaviour, IRing
    {
        public int Id { get; set; }
        public float Radius { get; private set; }
        public Vector2 Center { get; private set; }
        public float Weight { get; private set; }
        public float? WeightedPercentage { get; set; }
        public Vector2 ChildCenter { get; }
        
        public ScentRing()
        {
            
        }

        public void Initialize(int id, Vector2 center, float radius, float weight)
        {
            Id = id;
            Center = center;
            Radius = radius;
            Weight = weight;
        }
    }
}