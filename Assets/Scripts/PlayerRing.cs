using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.Serialization;
using Random = System.Random;

namespace LastKnownPosition
{
    public class PlayerRing : MonoBehaviour
    {
        private IList<IRing> _collidingRings = new List<IRing>();

        public double RingRadius = 1;

        public Vector2 Center;

        public GameObject CentreLine;
        private LineRenderer _centreLineRenderer;
        
        public GameObject LeftLine;
        private LineRenderer _leftLineRenderer;

        public GameObject RightLine;
        private LineRenderer _rightLineRenderer;

        public PlayerRing()
        {
            Center = new Vector2(0, 0);
        }
    
        // Start is called before the first frame update
        void Start()
        {
            _centreLineRenderer = CentreLine.GetComponent<LineRenderer>();
            _leftLineRenderer = LeftLine.GetComponent<LineRenderer>();
            _rightLineRenderer = RightLine.GetComponent<LineRenderer>();
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
            
            var lengthC = GetLengthBetweenPoints(Center, scentRing.Center);
            var lengthA = GetLengthBetweenPoints(new Vector2(Center.x, Center.y + 1), scentRing.Center);
            var lengthB = RingRadius;
            
            var angleA = CalculateCosineRule(lengthA, lengthB, lengthC);
            
            var degrees = angleA * 180 / Math.PI;

            var point = GetPoint(degrees, RingRadius);
            
            _centreLineRenderer.SetPosition(1, new Vector3(point.x, 0, point.y));

            var weightedRange = CalculateWeightedRange((scentRing as ScentRing).Weight);

            var random = new Random();
            var percentage = (scentRing as ScentRing).WeightSplit is null
                ? random.Next(1, 100) / 100d
                : (scentRing as ScentRing).WeightSplit;
            
            var minusAngle = degrees - (weightedRange * percentage);
            var leftPoint = GetPoint(minusAngle.Value, RingRadius);
            _leftLineRenderer.SetPosition(1, new Vector3(leftPoint.x, 0, leftPoint.y));
            
            var plusAngle = degrees + (weightedRange * (1 - percentage));
            var rightPoint = GetPoint(plusAngle.Value, RingRadius);
            _rightLineRenderer.SetPosition(1, new Vector3(rightPoint.x, 0, rightPoint.y));
            
            (scentRing as ScentRing).WeightSplit =  percentage;
        }

        private double CalculateWeightedRange(double weight)
        {
            var minimumDegrees = 36;
            return minimumDegrees * weight;
        }

        private double DegreesToRadians(double degrees)
        {
            return degrees * Math.PI / 180;
        }
        
        public Vector2 GetPoint(double deg, double distance)
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
                pointX += (float)(distance * Math.Sin(DegreesToRadians(deg)));
                pointY -= (float)(distance * Math.Cos(DegreesToRadians(deg)));
            }
            else if ((deg > 90) && (deg < 180))
            {
                pointX += (float)(distance * Math.Cos(DegreesToRadians(deg - 90)));
                pointY += (float)(distance * Math.Sin(DegreesToRadians(deg - 90)));
            }
            else if ((deg > 180) && (deg < 270))
            {
                pointX -= (float)(distance * Math.Sin(DegreesToRadians(deg - 180)));
                pointY += (float)(distance * Math.Cos(DegreesToRadians(deg - 180)));
            }
            else if ((deg > 270) && (deg < 360))
            {
                pointX -= (float)(distance * Math.Cos(DegreesToRadians(deg - 270)));
                pointY -= (float)(distance * Math.Sin(DegreesToRadians(deg - 270)));
            }

            return new Vector2((float)pointX, (float)pointY * -1);
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
            var cosTheta = (math.pow(lengthB, 2) + math.pow(lengthC, 2) - math.pow(lengthA, 2))/(2 * lengthB * lengthC);
            return math.acos(cosTheta);
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

