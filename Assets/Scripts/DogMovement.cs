
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Helpers;
using LastKnownPosition;
using UnityEngine;
using UnityEngine.AI;

public class DogMovement : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private Vector3 dogPosition;
    public Terrain Terrain; 
    
   //Dog Movement with player 
    public Transform player;
    private float OffsetX = 7f; //dog distance from player in X when following
    private float OffsetZ = 7f;//dog distance from player in Z when following
    private Vector3 velocity; 
    
    Vector3 lastTarget;

    public GameObject DogRing;
    private DogRing _dogRing;
    
    // Dog Searching in Radius
    private float radiusSearchDuration = 10f; //time the dog will search for
    
    private IList<Vector3> _points = new List<Vector3>();

    public Coroutine CurrentBehaviour;
    private float distanceToStartPoint = 1f;

    [SerializeField] private float maxDistanceFromPlayer;
   
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

    void Start()
    {
        _dogRing = DogRing.GetComponent<DogRing>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.T))
        {
            Debug.unityLogger.Log("Tracking Mode Starts");
            TrackScent();

            if (_points is not null && _points.First() != Vector3.zero)
            {
                UpdateBehaviour(DogState.GoToRadiusPoint);
            }
        }

        if (IsTooFarFromPlayer())
        {
            UpdateBehaviour(DogState.FollowPlayer);
        }
        
        SyncAgentToTransform();
    }

    private bool IsTooFarFromPlayer() => Vector3.Distance(player.position, transform.position) > maxDistanceFromPlayer;

    void TrackScent()
    {
        var scentRange = _dogRing.TrackScent();
        if (scentRange is null)
        {
            return;
        }
        
        SetPoints(scentRange.Points);
    }

    private void SetPoints(IList<Vector2> vector2Points)
    {
        _points = new List<Vector3>();

        foreach (var vector2Point in vector2Points)
        {
            _points.Add(ConvertPointToVector3(vector2Point));
        }
    }

    private Vector3 ConvertPointToVector3(Vector2 point)
    {
        var radiusPointATerrainHeight =
            Terrain.GetTerrainHeightAtPosition(
                player.position.x + point.x, 
                player.position.z + point.y);
        
        return new Vector3(player.position.x + point.x, radiusPointATerrainHeight, player.position.z + point.y);
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
                navMeshAgent.SetDestination(_points.First());

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
           Vector3 currentTarget;
           var counter = 0;
           var pointsAscending = true;
           while (searchDuration > elapsedTime)
           {
              if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= navMeshAgent.stoppingDistance) //if the path is not pending (finished calculating path) and the navMeshAgent is within the stopping distance of the destinations
              {
                  if (pointsAscending)
                  {
                      currentTarget = _points[++counter];
                      navMeshAgent.SetDestination(currentTarget);
                      
                      if (counter == _points.Count - 1)
                      {
                          pointsAscending = false;
                          continue;
                      }
                  }
                  else
                  {
                      currentTarget = _points[--counter];
                      navMeshAgent.SetDestination(currentTarget);
                      
                      if (counter == 0)
                      {
                          pointsAscending = true;
                          continue;
                      }
                  }
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

