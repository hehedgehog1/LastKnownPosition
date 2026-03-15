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
    public class DogRing : MonoBehaviour, IRing
    {
        private IList<ScentRing> _collidingRings = new List<ScentRing>();
        
        private TrackerManager _trackerManager;

        public Transform Player;
        
        public float Radius { get; } = 7.5f;
        public Vector2 Center { get; }
        
        // public GameObject LeftLine;
        // private LineRenderer _leftLineRenderer;
        //
        // public GameObject RightLine;
        // private LineRenderer _rightLineRenderer;

        public DogRing()
        {
            Center = new Vector2(0, 0);
        }
    
        // Start is called before the first frame update
        void Start()
        {
            _trackerManager = new TrackerManager();
            // _leftLineRenderer = LeftLine.GetComponent<LineRenderer>();
            // _rightLineRenderer = RightLine.GetComponent<LineRenderer>();
        }

        // Update is called once per frame
        void Update()
        {
            var pos = new Vector3(Player.position.x, Constants.RingOffset, Player.position.z);
        
            transform.SetPositionAndRotation(pos, Quaternion.identity);
        } 
        public ScentRange TrackScent()
        {
            if (_collidingRings.Count == 0)
            {
                return null;
            }

            var scentRing = _collidingRings.Last();
            
            var scentRange = _trackerManager.TrackScent(this, scentRing);
            return scentRange;
        }

        private void OnCollisionEnter(Collision collision)
        {
            var scentRing = collision.gameObject.GetComponent<ScentRing>();
            if (scentRing == null)
            {
                return;
            }
            
            if (!_collidingRings.Contains(scentRing))
            {
                _collidingRings.Add(scentRing);
            }
        }

        private void OnCollisionExit(Collision collision)
        {
            var scentRing = collision.gameObject.GetComponent<ScentRing>();
            if (scentRing == null)
            {
                return;
            }
            
            if (_collidingRings.Contains(scentRing))
            {
                _collidingRings.Remove(scentRing);
            }
        }
    }
}

