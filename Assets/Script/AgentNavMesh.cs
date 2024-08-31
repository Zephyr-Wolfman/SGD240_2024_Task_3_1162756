using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentNavMesh : MonoBehaviour
{
    [SerializeField]
    private GameObject[] waypoints;

    private Transform currentWaypoint;
    private Transform nextWaypoint;
    private Dictionary<int, int[]> waypointsDictiony;
    private int waypointIndex;
 
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        currentWaypoint = waypoints[Random.Range(0,11)].transform;
        transform.position = currentWaypoint.position;
        InitialiseWaypointDictionary();
    }



    private void Update()
    {
        PatrolPath();
        GetCurrentWaypoint(out waypointIndex);

    }

    private void PatrolPath()
    {
        navMeshAgent.destination = GetNextWaypoint().position;
    }

    // private Transform GetNextWaypoint()
    // {
    //     if (Vector3.Distance(transform.position, waypoints[0].transform.position) < 2)
    //     {
    //         int[] validIndices = { 1, 8 };
    //         nextWaypoint = waypoints[GetRandomWaypoint(validIndices)].transform;
    //     }

    //     else if (Vector3.Distance(transform.position, waypoints[1].transform.position) < 2)
    //     {
    //         int[] validIndices = { 0, 2, 10 };
    //         nextWaypoint = waypoints[GetRandomWaypoint(validIndices)].transform;
    //     }

    //     else if (Vector3.Distance(transform.position, waypoints[2].transform.position) < 2)
    //     {
    //         int[] validIndices = { 1, 3 };
    //         nextWaypoint = waypoints[GetRandomWaypoint(validIndices)].transform;
    //     }

    //     else if (Vector3.Distance(transform.position, waypoints[3].transform.position) < 2)
    //     {
    //         int[] validIndices = { 2, 4, 11 };
    //         nextWaypoint = waypoints[GetRandomWaypoint(validIndices)].transform;
    //     }

    //     else if (Vector3.Distance(transform.position, waypoints[4].transform.position) < 2)
    //     {
    //         int[] validIndices = { 3, 5 };
    //         nextWaypoint = waypoints[GetRandomWaypoint(validIndices)].transform;
    //     }

    //     else if (Vector3.Distance(transform.position, waypoints[5].transform.position) < 2)
    //     {
    //         int[] validIndices = { 4, 6, 11 };
    //         nextWaypoint = waypoints[GetRandomWaypoint(validIndices)].transform;
    //     }

    //     else if (Vector3.Distance(transform.position, waypoints[6].transform.position) < 2)
    //     {
    //         int[] validIndices = { 5, 7, 9 };
    //         nextWaypoint = waypoints[GetRandomWaypoint(validIndices)].transform;
    //     }

    //     else if (Vector3.Distance(transform.position, waypoints[7].transform.position) < 2)
    //     {
    //         int[] validIndices = { 6, 8 };
    //         nextWaypoint = waypoints[GetRandomWaypoint(validIndices)].transform;
    //     }

    //     else if (Vector3.Distance(transform.position, waypoints[8].transform.position) < 2)
    //     {
    //         int[] validIndices = { 7, 0, 9 };
    //         nextWaypoint = waypoints[GetRandomWaypoint(validIndices)].transform;
    //     }

    //     else if (Vector3.Distance(transform.position, waypoints[9].transform.position) < 2)
    //     {
    //         int[] validIndices = { 8, 10, 6 };
    //         nextWaypoint = waypoints[GetRandomWaypoint(validIndices)].transform;
    //     }

    //     else if (Vector3.Distance(transform.position, waypoints[10].transform.position) < 2)
    //     {
    //         int[] validIndices = { 9, 11, 1 };
    //         nextWaypoint = waypoints[GetRandomWaypoint(validIndices)].transform;
    //     }

    //     else if (Vector3.Distance(transform.position, waypoints[11].transform.position) < 2)
    //     {
    //         int[] validIndices = { 10, 3 };
    //         nextWaypoint = waypoints[GetRandomWaypoint(validIndices)].transform;
    //     }
    //     return nextWaypoint;
    // }

    private Transform GetNextWaypoint()
    {
        if (waypointsDictiony.ContainsKey(waypointIndex))
        {
            int[] validIndices = waypointsDictiony[waypointIndex];
            int pathOptions = GetRandomWaypoint(validIndices);
            nextWaypoint = waypoints[pathOptions].transform;
        }
        
        return nextWaypoint;
    }

    private int GetRandomWaypoint(params int[] optionalWaypoints)
    {
        int randomIndex = Random.Range(0, optionalWaypoints.Length);
        return optionalWaypoints[randomIndex];
    }

    private Transform GetCurrentWaypoint(out int index)
    {
        index = -1;
        for (int i = 0; i < waypoints.Length; i++)
        {
            if (Vector3.Distance(transform.position, waypoints[i].transform.position) < 2f)
            {
                currentWaypoint = waypoints[i].transform;
                index = i;
                break;
            }
        }
        return currentWaypoint;
    }

    private void InitialiseWaypointDictionary()
    {
        waypointsDictiony = new Dictionary<int, int[]>
        {
            {0, new int[] { 1, 8 } },
            {1, new int[] { 0, 2, 10 } },
            {2, new int[] { 1, 3 } },
            {3, new int[] { 2, 4, 11 } },
            {4, new int[] { 3, 5 } },
            {5, new int[] { 4, 6, 11 } },
            {6, new int[] { 5, 7, 9 } },
            {7, new int[] { 6, 8 } },
            {8, new int[] { 7, 0, 9 } },
            {9, new int[] { 8, 10, 6 } },
            {10, new int[] { 9, 11, 1 } },
            {11, new int[] { 10, 3 } }
        };

        
    }

  
}
