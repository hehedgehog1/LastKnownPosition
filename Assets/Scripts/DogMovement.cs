using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Vector3 dogPosition;
    
   //Dog Movement with player 
    public Transform player;
    private float OffsetX = 2f;
    private float OffsetZ = 2f;
    private Vector3 velocity;
    
//Dog Sniff
    private bool sniffMode = false;
    private float sniffDuration = 10f;
    private float sniffRadius = 5f;

    // Dog Searching in Radius
    private float sniffWeighting = 4f;
    private bool radiusSearchMode = false;
    private float radiusSearchDuration = 10f;
    private float searchSpeed = 5f;

    public Transform radiusPointA;
    public Transform radiusPointB;
   public Transform playerCenter; 
   private float speed = .1f;
    
   
 

  
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        navMeshAgent.updatePosition=false; 
    }


   
   

    // Update is called once per frame

   
  
  void Update()
  {
      if (Input.GetKeyDown(KeyCode.L))
      {
          sniffMode = true;
          radiusSearchMode = false;
          sniffDuration = 10f;
          Debug.unityLogger.Log("sniffMode");
      }

      
      float distanceFromPlayer = Vector3.Distance(player.position, transform.position);

      if (sniffMode)
      {
          MoveToRadius();

          if (distanceFromPlayer >= sniffRadius)
          {
              sniffMode = false;
              radiusSearchMode = true;
              radiusSearchDuration = 10f;
              
          }
      }
      else if (radiusSearchMode)
      {
          RadiusSearch();
      }
      else
      {
          FollowPlayer();
      }
      
      if (Input.GetKeyDown(KeyCode.L))
        {
            sniffMode = true;
            radiusSearchMode = false;
            sniffDuration = 10f;
            Debug.unityLogger.Log("sniffMode");
        }

  }

  void FollowPlayer()
    {
        Vector3 targetPosition = player.position + player.forward * OffsetZ + player.right * OffsetX;
        
        navMeshAgent.SetDestination(targetPosition);
        
        transform.position = Vector3.SmoothDamp(transform.position, navMeshAgent.nextPosition, ref velocity, 0.1f); 
    }
    
   
    void MoveToRadius()
    {
        Vector3 direction = (transform.position - player.position).normalized;
       Vector3 targetPosition = transform.position + direction * sniffRadius;

        navMeshAgent.SetDestination(targetPosition);
        
        
        
        
    }
  

  void RadiusSearch()
  {
      Vector3 radiusA = new Vector3(radiusPointA.position.x, 0f, radiusPointA.position.z) - new Vector3 (playerCenter.position.x,0f,playerCenter.position.z);
      Vector3 radiusB = new Vector3(radiusPointB.position.x, 0f, radiusPointB.position.z) - new Vector3 (playerCenter.position.x,0f,playerCenter.position.z);
        
      float t = Mathf.PingPong(Time.time*speed, 1f) ;
        
      
      transform.position = Vector3.Slerp(radiusA, radiusB, t);
      transform.position += playerCenter.position;


      navMeshAgent.SetDestination(transform.position);


      radiusSearchDuration -= Time.deltaTime;
      if (radiusSearchDuration <= 0f)
      {
          radiusSearchMode = false;
      }
  }


}