using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Patrolling : MonoBehaviour
{
    [SerializeField]
    private GameObject[] waypoints;
    private Transform currentWaypoint;
    private Transform nextWaypoint;
    private Dictionary<int, int[]> waypointsDictionary;
    private int waypointIndex;
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        currentWaypoint = waypoints[Random.Range(0, 11)].transform;
        transform.position = currentWaypoint.position;
        InitialiseWaypointsDictionary();
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

    private Transform GetNextWaypoint()
    {
        if (waypointsDictionary.ContainsKey(waypointIndex))
        {
            int[] validIndices = waypointsDictionary[waypointIndex];
            int pathOptions = GetRandomWaypoint(validIndices);
            nextWaypoint = waypoints[pathOptions].transform;
        }
        
        return nextWaypoint;
    }

    private int GetRandomWaypoint(params int[] waypointOptions)
    {
        int randomIndex = Random.Range(0, waypointOptions.Length);
        return waypointOptions[randomIndex];
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

    private void InitialiseWaypointsDictionary()
    {
        waypointsDictionary = new Dictionary<int, int[]>
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
