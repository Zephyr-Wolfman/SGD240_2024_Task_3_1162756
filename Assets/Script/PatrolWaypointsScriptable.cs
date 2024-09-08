using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class PatrolWaypointsScriptable : ScriptableObject
{
    public Dictionary<int, int[]> waypointsDictionary;

    public void InitialiseWaypointsDictionary()
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
