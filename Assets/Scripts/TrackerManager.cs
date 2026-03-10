using System;
using Unity.Mathematics;
using UnityEngine;
using Random = System.Random;

namespace LastKnownPosition
{
    public class TrackerManager
    {
        public (Vector2, Vector2) TrackScent(DogRing dogRing, ScentRing scentRing)
        {
            var centerRadianAngle = GetRadianAngleOfCenterLine(dogRing, scentRing);
            var centerDegreeAngle = GetRadiansToDegrees(centerRadianAngle);
            
            var weightedRange = GetWeightedRange(scentRing.Weight);
            var weightedPercentage = GetWeightedPercentage(scentRing.WeightedPercentage);
            scentRing.WeightedPercentage = weightedPercentage;
            
            var point1Angle = centerDegreeAngle - weightedRange * weightedPercentage;
            var point1 = GetPointOnCircumference(point1Angle, dogRing.Radius);
            
            var point2Angle = centerDegreeAngle + weightedRange * (1 - weightedPercentage);
            var point2 = GetPointOnCircumference(point2Angle, dogRing.Radius);
            
            return (point1, point2);
        }

        private float GetRadianAngleOfCenterLine(DogRing dogRing, ScentRing scentRing)
        {
            var lengthC = GetLengthBetweenPoints(
                dogRing.Center, 
                scentRing.Center);
            var lengthA = GetLengthBetweenPoints(
                new Vector2(dogRing.Center.x, dogRing.Center.y + dogRing.Radius), 
                scentRing.Center);
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
            else if (deg == 90)
            {
                pointX += distance;
            }
            else if (deg == 180)
            {
                pointY += distance;
            }
            else if (deg == 270)
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
        
        private float GetWeightedRange(float weight) => Constants.BaseDirectionRange * weight;
    }
}