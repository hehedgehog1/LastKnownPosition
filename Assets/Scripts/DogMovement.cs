
using System.Collections;
using UnityEngine;
using UnityEngine.AI;

public class DogMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Vector3 dogPosition;
    
   //Dog Movement with player 
    public Transform player;
    private float OffsetX = 2f; //dog distance from player in X when following
    private float OffsetZ = 1.5f;//dog distance from player in Z when following
    private Vector3 velocity; 
    
    Vector3 lastTarget;
    
    
    
    // Dog Searching in Radius
    private float radiusSearchDuration = 20f; //time the dog will search for
   

    public Transform radiusPointA;
    public Transform radiusPointB;

    public Coroutine CurrentBehaviour;
    private float distanceToStartPoint = 1f;
   
   
   public enum DogState
   {
       FollowPlayer,
       GoToRadiusPoint,
       Tracking
   }

   public DogState MyState; 

  
    private void Awake()
    { 
        navMeshAgent = GetComponent<NavMeshAgent>();
      navMeshAgent.updatePosition=false; 
      UpdateBehaviour(DogState.FollowPlayer);
      
     
      navMeshAgent.updateRotation = true;
    }


    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.unityLogger.Log("Tracking Mode Starts");
            UpdateBehaviour(DogState.GoToRadiusPoint);
        }
        
        

        SyncAgentToTransform();

    }

    void SyncAgentToTransform()
        {
           
            transform.position = navMeshAgent.nextPosition;
        }

        void UpdateBehaviour(DogState state)
        {
            MyState = state;

            if (CurrentBehaviour != null) // Stops coroutine of current behaviour when state changes
            {
                StopCoroutine(CurrentBehaviour);
            }

            switch (MyState)
            {
                case DogState.FollowPlayer:
                    CurrentBehaviour = StartCoroutine(FollowPlayer()); // follow player state
                    break;
                case DogState.GoToRadiusPoint:
                    CurrentBehaviour = StartCoroutine(MoveToPointA()); //start of tracking, dog moves to first point on search 
                    break;
                case DogState.Tracking:
                    CurrentBehaviour = StartCoroutine(Tracking()); //tracking, dog moves between search points
                    break;
            }


        }

        IEnumerator FollowPlayer()
        {
            while (true)
            {
                Vector3 targetPosition = player.position + player.forward * OffsetZ + player.right * OffsetX; //follow player with slight offset so dog is visible
               
                if (Vector3.Distance(lastTarget, targetPosition) > 0.5f)
                {
                    navMeshAgent.SetDestination(targetPosition);
                    lastTarget = targetPosition;
                }
                transform.position =
                    Vector3.SmoothDamp(transform.position, navMeshAgent.nextPosition, ref velocity, 0.1f); // smoothes motion
              

                yield return null;
                navMeshAgent.nextPosition = transform.position;
                
            }

        }

        IEnumerator MoveToPointA()
        {

            while (true)
            {
               Vector3 midPoint =  (radiusPointA.position + radiusPointB.position)/2f; //midpoint between two tracking points
                navMeshAgent.SetDestination(midPoint);

              if (navMeshAgent.remainingDistance <= distanceToStartPoint) //once the dog reaches destination, it will start tracking by moving between two points
                {
                    UpdateBehaviour(DogState.Tracking);
                }

                yield return null;
            }
        }
        
        IEnumerator Tracking()
        {
            float elapsedTime = 0f;
            float searchDuration = radiusSearchDuration;
           navMeshAgent.updatePosition = true; 
        
          
           navMeshAgent.autoBraking = false; //stops dog slowing down as it reaches desitnation
           Transform currentTarget = radiusPointA;
            while (searchDuration > elapsedTime)
            {
                
              
              if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) //if the path is not pending (finished calculating path) and the navMeshAgent is within the stopping distance of the destinations
              {
                  if (currentTarget == radiusPointA) //if the navMeshAgent has reached A, go to B/if the agent has reach B, go to A
                      currentTarget = radiusPointB;
                  else
                      currentTarget = radiusPointA;
                  navMeshAgent.SetDestination(currentTarget.position);
              }
              
              yield return null;
              elapsedTime += Time.deltaTime; 
             

            }
            
            if (searchDuration <= elapsedTime)
            {
                UpdateBehaviour(DogState.FollowPlayer);
                navMeshAgent.autoBraking = true;
            }
           
        }

    }

