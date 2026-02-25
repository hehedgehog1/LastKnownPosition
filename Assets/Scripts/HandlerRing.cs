using System.Collections.Generic;
using UnityEngine;

namespace LastKnownPosition
{
    public class HandlerRing : MonoBehaviour
    {
        public IList<Collision> Collisions = new List<Collision>();

        public float Radius;

        public Vector2 Center;
    
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        private void OnCollisionEnter(Collision collidingScentRing)
        {
            if (!Collisions.Contains(collidingScentRing))
            {
                Collisions.Add(collidingScentRing);
            }
        }

        private void OnCollisionExit(Collision collidingScentRing)
        {
            if (Collisions.Contains(collidingScentRing))
            {
                Collisions.Remove(collidingScentRing);
            }
        }
    }
}
