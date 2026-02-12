using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;

namespace LastKnownPosition
{
    public class PlayerRing : MonoBehaviour
    {
        private IList<IRing> _collidingRings = new List<IRing>();

        public double RingRadius = 1;

        public Vector2 Center;

        public PlayerRing()
        {
            Center = new Vector2(0, 0);
        }
    
        // Start is called before the first frame update
        void Start()
        {
            var test = 5;
        }

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.T))
            {
                TrackScent();
            }
        }

        private void TrackScent()
        {
            if (_collidingRings.Count == 0)
            {
                return;
            }

            var scentRing = _collidingRings.Last();
            
            var centreToCentre = GetLengthBetweenPoints(Center, scentRing.Center);
            var radiusToCentre = GetLengthBetweenPoints(new Vector2(Center.x, Center.y + 1), scentRing.Center);
            
            var angle = CalculateCosineRule(1, radiusToCentre, centreToCentre);

            var circumferencePosition = CalculatePositionOnCircumference(angle);
        }

        private double GetLengthBetweenPoints(Vector2 pointA, Vector2 pointB)
        {
            var width = pointB.x - pointA.x;
            var height = pointB.y - pointA.y;
            
            return CalculateHypotenuse(width, height);
        }

        private double CalculateHypotenuse(double width, double height)
        {
            return math.sqrt(width * width + height * height);
        }

        private double CalculateCosineRule(double lengthA, double lengthB, double lengthC)
        {
            var cosTheta = (math.pow(lengthA, 2) + math.pow(lengthB, 2) - math.pow(lengthC, 2))/(2 * lengthA * lengthB);
            return math.acos(cosTheta);
        }

        private Vector2 CalculatePositionOnCircumference(double angle)
        {
            var x = Center.x + 1 * math.cos(angle);
            var y = Center.y + 1 * math.sin(angle);
            return new Vector2((float)x, (float)y);
        }

        private void OnCollisionEnter(Collision collision)
        {
            var scentRing = collision.gameObject.GetComponent<ScentRing>();
            if (!_collidingRings.Contains(scentRing))
            {
                _collidingRings.Add(scentRing);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            var scentRing = collision.gameObject.GetComponent<ScentRing>();
            if (_collidingRings.Contains(scentRing))
            {
                _collidingRings.Remove(scentRing);
            }
        }
    }
}

