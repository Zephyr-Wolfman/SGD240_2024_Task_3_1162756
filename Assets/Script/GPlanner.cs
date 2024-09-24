using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GPlanner
{

    // public static Vector3 MakePlan(GGoalSO goal)
    // {
    //     return goal.Actions[0].GetLocation();
    // }

    public static Queue<PlannedAction> MakePlan(List<GActionSO> actions)
    {
        Queue<PlannedAction> actionQueue = new Queue<PlannedAction>();

        foreach (GActionSO action in actions)
        {
            PlannedAction plannedAction = new PlannedAction
            {
                actionName = action.ActionName,
                duration = action.ActionDuration,
                targetPosition = action.GetLocation()
            };

            actionQueue.Enqueue(plannedAction);
        }
        return actionQueue;
    }

    public struct PlannedAction
    {
        public string actionName;
        public float duration;
        public Vector3 targetPosition;
    }
}

