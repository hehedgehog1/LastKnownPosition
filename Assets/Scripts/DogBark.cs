using UnityEngine; 
public class DogBarkController : MonoBehaviour 
{ 
    public Transform pointA; 
    public Transform pointB; 
    public AudioSource barkSound; 

    void Update() 
    { 
        float dogX = transform.position.x; 
        float minX = Mathf.Min(pointA.position.x, pointB.position.x); 
        float maxX = Mathf.Max(pointA.position.x, pointB.position.x); 
        bool isBetweenPoints = dogX >= minX && dogX <= maxX; 
        if (isBetweenPoints && !barkSound.isPlaying) 
        { 
            barkSound.Play(); 
        } 
    } 
}
