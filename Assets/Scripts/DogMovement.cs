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
    
    
//Dog Sniff
    private bool sniffMode = false;
    private float sniffDuration = 10f;
    private float sniffRadius = 5f;

    // Doge Searching in Radius
    private float sniffWeighting = 4f;
    private bool radiusSearchMode = false;
    private float radiusSearchDuration = 10f;
    


 

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {

            sniffMode = true;
            sniffDuration = 10f;
            
        }

        if (sniffMode)
        {
            MoveToRadius();
            sniffDuration -= Time.deltaTime;
            if (sniffDuration <= 0)
            {
                sniffMode = false;
            }
        }
        else
        {
            FollowPlayer();
        }
       
    }

    void FollowPlayer()
    {
        Vector3 targetPosition = player.position + player.forward * OffsetZ + player.right * OffsetX;
        navMeshAgent.SetDestination(targetPosition);
    }
    void MoveToRadius()
    {
        Vector3 direction = (transform.position - player.position).normalized;
        Vector3 targetPosition = transform.position + direction * sniffRadius;

        navMeshAgent.SetDestination(targetPosition);
        
        
    }
   /* void RadiusSeach()
    {
        radiusSearchMode = true;
    }*/
}