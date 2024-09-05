using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class WaypointsVisualiser : MonoBehaviour
{
    [SerializeField]
    private Color color;

    private void OnDrawGizmos()
    {
        foreach (Transform t in transform)
        {
            Gizmos.color = color;
            Gizmos.DrawSphere(t.position, 0.5f);
        }
    }
}
