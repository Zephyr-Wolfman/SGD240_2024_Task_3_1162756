using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GPlanner
{

    public static Vector3 MakePlan(GGoalSO goal)
    {
        return goal.Actions[0].GetLocation();
    }


}
