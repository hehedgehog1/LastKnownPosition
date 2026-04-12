
using Models;
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
        public Vector2 ChildCenter { get; private set; }

        public void Initialize(int id, Vector2 center, float radius, float weight, Location? childLocation = null)
        {
            Id = id;
            Center = center;
            Radius = radius;
            Weight = weight;

            if (childLocation != null)
            {
                ChildCenter = new Vector2(childLocation.X, childLocation.Z);
            }
            
        }
    }
}