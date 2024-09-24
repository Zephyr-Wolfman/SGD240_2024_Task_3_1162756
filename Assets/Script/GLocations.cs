using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GLocations : MonoBehaviour
{
    [SerializeField]
    private GameObject[] waypoints;
    [SerializeField]
    private GameObject[] rooms;
    [SerializeField]
    private GameObject[] agents;
    private Vector3 currentWaypoint;
    private Vector3 nextWaypoint;
    private int waypointIndex;
    // private bool atNextWayPoint = false;
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

    private void Awake()
    {
        InitPatrolWaypoints();
    }


    // Get the next waypoint based on the current waypoint index
    public Vector3 GetNextWaypoint(string actionName)
    {
        switch (actionName)
        {
            case "UseToilet":
                return rooms[0].transform.position;
            case "MakeCoffee":
                return rooms[1].transform.position;
            case "Sleep":
                return rooms[2].transform.position;
            case "ChaseRat":
                return agents[1].transform.position;
            // case "GoToNextPatrolPoint":
            //     return GetNextPatrolLocation();
            default:
                // Debug.Log("No action detected!");
                return Vector3.zero;
        }



    }

    public Vector3 GetNextPatrolLocation()
    {
        foreach (GameObject agent in agents)
        {
            Vector3 pos = agent.transform.position;
            RandomGenerator randomGenerator = new RandomGenerator();

            waypointIndex = GetCurrentWaypoint(pos);

            if (patrolWaypointsDict.ContainsKey(waypointIndex) && Vector3.Distance(pos, waypoints[waypointIndex].transform.position) < 1)
            {
                int[] validIndices = patrolWaypointsDict[waypointIndex];
                int chosenWaypoint = randomGenerator.GetRandomWaypoint(validIndices);
                // Debug.Log($"Next Waypoint = {chosenWaypoint}");
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
        return nextWaypoint;
    }

    //Select random waypoint from valid options
    // private int GetRandomWaypoint(params int[] waypointOptions)
    // {
    //     int randomIndex = Random.Range(0, waypointOptions.Length);
    //     return waypointOptions[randomIndex];

    // }

    // Find the waypoint the agent is closest to
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

        currentWaypoint = waypoints[index].transform.position;
        // Debug.Log("index = " + index);
        return index;
    }

    private void InitPatrolWaypoints()
    {
        patrolWaypointsDict = new Dictionary<int, int[]>
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

public class RandomGenerator
{
    public int GetRandomWaypoint(params int[] waypointOptions)
    {
        int randomIndex = Random.Range(0, waypointOptions.Length);
        return waypointOptions[randomIndex];

    }

}
