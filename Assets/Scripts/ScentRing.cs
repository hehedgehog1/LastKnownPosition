
using UnityEngine;

namespace LastKnownPosition
{
    public class ScentRing : MonoBehaviour, IRing
    {
        public double Radius { get; }
        public Vector2 Center { get; }
        public double Weight { get; }
        public Vector2 ChildCenter { get; }
        
        public ScentRing()
        {
            Center = new Vector2(5, 3);
            Radius = 0.5;
        }
    }
}