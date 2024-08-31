using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentNavMesh : MonoBehaviour
{
    [SerializeField]
    private GameObject[] waypoints;

    // [SerializeField]
    // private Transform movePositionTransform;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        // navMeshAgent.destination = movePositionTransform.position;
        PatrolPath();
    }

    private void PatrolPath()
    {
        navMeshAgent.destination = waypoints[0].transform.position;
        if (transform.position == waypoints[0].transform.position)
        {
            navMeshAgent.destination = waypoints[1].transform.position;
        }
    }
}
