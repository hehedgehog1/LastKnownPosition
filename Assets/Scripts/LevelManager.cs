using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;

namespace LastKnownPosition
{
    public class LevelManager : MonoBehaviour
    {
        private GameObject _handler;
        private GameObject _missingPerson;

        public LevelManager()
        {
           // _handler = new Handler();
        }
        
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        void Update()
        {
        
        }


        public void TrackScent()
        {
            // Check is Handler Ring colliding with Scent Rings
            //TODO: Get HandlerRing out of Player entity

            // if (!_handler.DogRing.Collisions.Any())
            // {
            //     return;
            // }
            //
            // var TriangleCoord1 = _handler.DogRing.Center;
            // var TriangleCoord2 = new Vector2(_handler.DogRing.Center.x, _handler.DogRing.Center.y + _handler.DogRing.Radius);
            //
            // //TODO: pull this out of scent ring
            // var scentRingCenter = _missingPerson.ScentRings.First().ChildCenter;
            //
            // var triangleCoord3 = scentRingCenter;
            //
            //
            // var aLength = _handler.DogRing.Radius;
            // var bLength = GetLengthOfLine(TriangleCoord1, scentRingCenter);
            // var cLength = GetLengthOfLine(TriangleCoord2, scentRingCenter);
            
            
            
        }

        // private float GetLengthOfLine(Vector2 a, Vector2 b)
        // {
        //     
        // }

        private float GetAngle(float aLength, float bLength, float cLength)
        {
            // Cosine Rule
            var bSquared = math.pow(bLength, 2);
            var cSquared = math.pow(cLength, 2);
            var aSquared = math.pow(aLength, 2);

            var cosA = (bSquared + cSquared - aSquared)/(2 * bLength * cLength);
            
            return math.acos(cosA);
        }
        
        private double DegreeToRadian(double angle)
        {
            return Math.PI * angle / 180.0;
        }

        private double RadianToDegree(double angle)
        {
            return angle * (180.0 / Math.PI);
        }
    }
}


