using System;
using Helpers;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

namespace LastKnownPosition
{
    public class TrackerManager
    {
        public ScentRange TrackScent(DogRing dogRing, ScentRing scentRing)
        {
            var scentRange = new ScentRange();
            
            var triangleThetaRadians = GetRadianAngleOfCenterLine(dogRing, scentRing);
            var triangleThetaDegrees = GetRadiansToDegrees(triangleThetaRadians);
            var centerDegreeAngle = CompensateForTriangleFlipping(dogRing, scentRing, triangleThetaDegrees);
            
            var weightedRange = GetWeightedRange(scentRing.Weight);
            var weightedPercentage = GetWeightedPercentage(scentRing.WeightedPercentage);
            scentRing.WeightedPercentage = weightedPercentage;

            var pointAAngle = (centerDegreeAngle - weightedRange * weightedPercentage)
                .Standardise();
            var pointA = GetPointOnCircumference(pointAAngle, dogRing.Radius);
            scentRange.Points.Add(pointA);

            var innerSegmentAngle = GetInnerSegmentAngle(weightedRange);
            var innerSegmentAngleA = (pointAAngle + innerSegmentAngle).Standardise();
            var innerSegmentPointA = GetPointOnCircumference(innerSegmentAngleA, dogRing.Radius);
            scentRange.Points.Add(innerSegmentPointA);
            
            var innerSegmentAngleB = (innerSegmentAngleA + innerSegmentAngle).Standardise();
            var innerSegmentPointB = GetPointOnCircumference(innerSegmentAngleB, dogRing.Radius);
            scentRange.Points.Add(innerSegmentPointB);
            
            
            var pointBAngle = (centerDegreeAngle + weightedRange * (1 - weightedPercentage))
                .Standardise();
            var pointB = GetPointOnCircumference(pointBAngle, dogRing.Radius);
            scentRange.Points.Add(pointB);

            return scentRange;
        }

        private float CompensateForTriangleFlipping(DogRing dogRing, ScentRing scentRing, float angle)
        {
            if (dogRing.gameObject.transform.position.x > scentRing.gameObject.transform.position.x)
            {
                return 360 - angle;
            }
            
            return angle;
        }
        
        private float GetInnerSegmentAngle(float weightedRange) => weightedRange / 3;

        private float GetRadianAngleOfCenterLine(DogRing dogRing, ScentRing scentRing)
        {
            var targetCenter = scentRing.ChildCenter ?? scentRing.Center;
            
            var lengthC = GetLengthBetweenPoints(
                new Vector2(dogRing.gameObject.transform.position.x, dogRing.gameObject.transform.position.z),
                targetCenter);
            var lengthA = GetLengthBetweenPoints(
                new Vector2(dogRing.gameObject.transform.position.x, dogRing.gameObject.transform.position.z + dogRing.Radius), 
                targetCenter);
            var lengthB = dogRing.Radius;
            
            var radianAngle = GetCosineRule(lengthA, lengthB, lengthC);
            return radianAngle;
        }

        private float GetWeightedPercentage(float? weightSplit)
        {
            var random = new Random();
            var percentage = weightSplit ?? random.Next(1, 100) / 100f;
            return percentage;
        }
        
        private float GetCosineRule(float lengthA, float lengthB, float lengthC)
        {
            var cosTheta = (math.pow(lengthB, 2) + math.pow(lengthC, 2) - math.pow(lengthA, 2))/(2 * lengthB * lengthC);
            return math.acos(cosTheta);
        }
        
        private float GetLengthBetweenPoints(Vector2 pointA, Vector2 pointB)
        {
            var width = pointB.x - pointA.x;
            var height = pointB.y - pointA.y;
            
            return GetHypotenuse(width, height);
        }

        private float GetHypotenuse(float width, float height)
        {
            return math.sqrt(width * width + height * height);
        }

        private float GetRadiansToDegrees(float radians) => radians * 180 / math.PI;
        
        private double GetDegreesToRadians(double degrees) => degrees * math.PI / 180;
        
        private Vector2 GetPointOnCircumference(float deg, float distance)
        {
            var pointX = 0d;
            var pointY = 0d;

            if (deg == 0)
            {
                pointY -= distance;
            }
            else if (Mathf.Approximately(deg, 90))
            {
                pointX += distance;
            }
            else if (Mathf.Approximately(deg, 180))
            {
                pointY += distance;
            }
            else if (Mathf.Approximately(deg, 270))
            {
                pointX -= distance;
            }
            else if ((deg > 0) && (deg < 90))
            {
                pointX += (float)(distance * Math.Sin(GetDegreesToRadians(deg)));
                pointY -= (float)(distance * Math.Cos(GetDegreesToRadians(deg)));
            }
            else if ((deg > 90) && (deg < 180))
            {
                pointX += (float)(distance * Math.Cos(GetDegreesToRadians(deg - 90)));
                pointY += (float)(distance * Math.Sin(GetDegreesToRadians(deg - 90)));
            }
            else if ((deg > 180) && (deg < 270))
            {
                pointX -= (float)(distance * Math.Sin(GetDegreesToRadians(deg - 180)));
                pointY += (float)(distance * Math.Cos(GetDegreesToRadians(deg - 180)));
            }
            else if ((deg > 270) && (deg < 360))
            {
                pointX -= (float)(distance * Math.Cos(GetDegreesToRadians(deg - 270)));
                pointY -= (float)(distance * Math.Sin(GetDegreesToRadians(deg - 270)));
            }

            return new Vector2((float)pointX, (float)pointY * -1);
        }
        
        private float GetWeightedRange(float weight) => Math.Min(Constants.BaseDirectionRange * weight, 180);
    }
}