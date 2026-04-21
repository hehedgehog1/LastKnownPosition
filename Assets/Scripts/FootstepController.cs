using System;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class FootstepController : MonoBehaviour
{
    [Range(0, 20f)] public float frequency = 10.0f;
    
    [SerializeField] public List<AudioClip> footsteps;

    private float _sin;
    
    private bool _isTriggered;
    
    [SerializeField]
    private AudioSource _audioSource;
    
    void Update()
    {
        var inputMagnitude = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical")).magnitude;

        
        if (inputMagnitude > 0)
        {
            StartFootsteps();
        }
    }

    private void StartFootsteps()
    {
        _sin = Mathf.Sin(Time.time * frequency);

        if (_sin > 0.97f && !_isTriggered)
        {
            _isTriggered = true;
            var ran = new Random();
            var audioNum = ran.Next(0, footsteps.Count - 1);
            _audioSource.clip = footsteps[audioNum];
            _audioSource.Play();
        }
        else if (_isTriggered && _sin < -0.97f)
        {
            _isTriggered = false;
        }
    }
}
