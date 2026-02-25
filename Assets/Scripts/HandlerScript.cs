using System;
using LastKnownPosition;
using UnityEngine;


    public class HandlerScript : MonoBehaviour
    {
        public event EventHandler TrackScent;
        
        public DogRing DogRing { get; private set; }
        

        // Update is called once per frame
        void Update()
        {
            if (Input.GetKeyDown("t"))
            {
                TrackScent?.Invoke(this, EventArgs.Empty);
            }
        }
    }
