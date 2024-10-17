using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

/// <summary> 
/// Manages locations for agents and actions.
/// </summary>
public class GLocations : MonoBehaviour
{
    [SerializeField]
    private GameObject[] waypoints;
    [SerializeField]
    private GameObject[] roomObjs;
    [SerializeField]
    private GameObject[] agents;
    [SerializeField]
    private GPatrolWaypointsSO patrolWaypoints;
    private GWorldStates worldStates;
    private int waypointIndex;
    private Dictionary<int, int[]> patrolWaypointsDict;

    private static GLocations instance;
    public static GLocations Instance
    {
        get
        {
            if (instance == null)
            {
                instance = FindObjectOfType<GLocations>();

                if (instance == null)
                {
                    Debug.Log("No GLocations Object Found");
                }
            }
            return instance;
        }
    }

    // Initialises patrol waypoints dictionary and world states instance
    private void Awake()
    {
        patrolWaypointsDict = patrolWaypoints.InitPatrolWaypoints();
        worldStates = GWorldStates.Instance;
    }


    // Get the next location based on the current waypoint index
    public Vector3 GetNextWaypoint(string actionName, GameObject agent)
    {
        switch (actionName)
        {
            case "UseToilet":
                return roomObjs[0].transform.position;
            case "MakeCoffee":
                return roomObjs[1].transform.position;
            case "DrinkCoffee":
                return roomObjs[1].transform.position;
            case "Sleep":
                return roomObjs[2].transform.position;
            case "ChaseRat":
                return agents[0].transform.position;
            case "Patrol":
                return GetNextPatrolLocation(agent);
            default:
                // Debug.Log("No action detected!");
                return Vector3.zero;
        }

    }

    // Gets the next patrol waypoint for the agent
    public Vector3 GetNextPatrolLocation(GameObject agent)
    {
        Vector3 pos = agent.transform.position;
        NavMeshAgent navMeshAgent = agent.GetComponent<NavMeshAgent>();
        RandomGenerator randomGenerator = new RandomGenerator();

        waypointIndex = GetCurrentWaypoint(pos);
        Vector3 nextWaypoint = waypoints[1].transform.position;


        if (patrolWaypointsDict.ContainsKey(waypointIndex) && navMeshAgent.remainingDistance < 2f)
        {
            int[] validIndices = patrolWaypointsDict[waypointIndex];
            int chosenWaypoint = randomGenerator.GetRandomWaypoint(validIndices);
            Debug.Log($"Next Waypoint = {chosenWaypoint}");
            if (waypoints[chosenWaypoint] != null)
            {
                nextWaypoint = waypoints[chosenWaypoint].transform.position;
            }
            return nextWaypoint;
        }
        else
        {

            return nextWaypoint;
        }
    }

    // Finds the waypoint the agent is closest to
    private int GetCurrentWaypoint(Vector3 pos)
    {
        int index = 0;

        for (int i = 0; i < waypoints.Length; i++)
        {
            if (Vector3.Distance(pos, waypoints[i].transform.position) < 2f)
            {
                index = i;
            }
        }

        Vector3 currentWaypoint = waypoints[index].transform.position;
        // Debug.Log("index = " + index);
        return index;
    }

    // Checks if agent is near a waypoint
    public bool IsAtAWaypoint(Vector3 pos)
    {
        foreach (var waypoint in waypoints)
        {
            if (Vector3.Distance(pos, waypoint.transform.position) < 2)
            {
                return true;
            }
        }
        return false;
    }
}

/// <summary> 
/// Generates a random waypoint within a range
/// </summary>
public class RandomGenerator
{
    public int GetRandomWaypoint(params int[] waypointOptions)
    {
        int randomIndex = Random.Range(0, waypointOptions.Length);
        return waypointOptions[randomIndex];

    }

}

