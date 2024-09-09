using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAgent : GAgentBase
{

    private void Update()
    {
        Move();
    }

    public void Move()
    {
        navMeshAgent.destination = actions[0].location;
    }
}
