using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class DogMovement : MonoBehaviour
{
    public Transform player;

    private NavMeshAgent navMeshAgent;

    private float OffsetX = 2f;
    private float OffsetZ = 2f;



    private Vector3 dogPosition;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();

    }

    // Update is called once per frame
    void FixedUpdate()
    {

        if (player != null)
        {
            Vector3 targetPosition = player.position + player.forward * OffsetZ + player.right * OffsetX;
            navMeshAgent.SetDestination(targetPosition);

        }
    }
}