using UnityEngine;

public class CircularMotion : MonoBehaviour
{
    public Transform radiusPointA;
    public Transform radiusPointB;
    public Transform playerCenter; 
    
    public float searchTime;

    private float startTime;

    private float speed = 1f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        startTime = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
       
        
        Vector3 radiusA = new Vector3(radiusPointA.position.x, 0f, radiusPointA.position.z) - new Vector3 (playerCenter.position.x,0f,playerCenter.position.z);
        Vector3 radiusB = new Vector3(radiusPointB.position.x, 0f, radiusPointB.position.z) - new Vector3 (playerCenter.position.x,0f,playerCenter.position.z);
        
        float t = Mathf.PingPong(Time.time*speed, 1f) ;
        
      
        transform.position = Vector3.Slerp(radiusA, radiusB, t);
        transform.position += playerCenter.position;
        
        
        

    }
}
