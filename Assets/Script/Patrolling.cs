using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary>
/// Controls agent's patrolling behaviour between waypoints using the NavMeshAGent. 
///  It allows the agent to update its destination as it reaches each waypoint
/// </summary>

public class Patrolling : MonoBehaviour
{
    [SerializeField]
    private GameObject[] waypoints;
    private Transform currentWaypoint;
    private Transform nextWaypoint;
    private Dictionary<int, int[]> waypointsDictionary;
    private int waypointIndex;
    private NavMeshAgent navMeshAgent;

    // Initialise NavMeshAgent in Awake so it's available before other components need it
    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Set the initial waypoint and populate the waypoints dictionary
    private void Start()
    {
        // Set the current waypoint to a random waypoint
        currentWaypoint = waypoints[Random.Range(0, waypoints.Length)].transform;
        // Set the location of the object to the current waypoint
        transform.position = currentWaypoint.position;
        InitialiseWaypointsDictionary();
    }

    // Keep udating the current waypoint and patrol path
    private void Update()
    {
        GetCurrentWaypoint(out waypointIndex);
        PatrolPath();
    }

    // Set the destination to the next waypoint for patrolling
    private void PatrolPath()
    {
        if (nextWaypoint != null)
        {
            navMeshAgent.destination = GetNextWaypoint().position;
        }
        else
        {
            nextWaypoint = GetNextWaypoint();
            if (nextWaypoint != null)
            {
                navMeshAgent.destination = nextWaypoint.position;
            }
        }
    }
    // Get the next waypoint based on the current waypoint index
    private Transform GetNextWaypoint()
    {
        if (waypointsDictionary.ContainsKey(waypointIndex))
        {
            int[] validIndices = waypointsDictionary[waypointIndex];
            int pathOptions = GetRandomWaypoint(validIndices);

            if (waypoints[pathOptions] != null)
            {
                nextWaypoint = waypoints[pathOptions].transform;
            }
        }

        return nextWaypoint;
    }

    // Select random waypoint from valid options
    private int GetRandomWaypoint(params int[] waypointOptions)
    {
        int randomIndex = Random.Range(0, waypointOptions.Length);
        return waypointOptions[randomIndex];
    }

    // Find the waypoint the agent is closest to
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

    // Initialise the dictionary that defines the valid paths from specific waypoints
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
