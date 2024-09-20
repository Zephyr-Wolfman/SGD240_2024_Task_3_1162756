using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class GAgentBase : MonoBehaviour
{
    [SerializeField]
    protected float energyLevel = 1f;
    [SerializeField]
    protected float bladderLevel = 1f;
    [SerializeField]
    protected float moraleLevel = 1f;
    [SerializeField]
    protected float patrolQuota = 0.5f;
    [SerializeField]
    protected bool ratDetected = false;
    [SerializeField]
    protected int currentGoal = 0;

    // [SerializeField]
    // protected GActionSO[] actions;
    [SerializeField]
    protected List<GGoalSO> goals = new List<GGoalSO>();

    protected GWorldStates worldStates;

    protected UnityEngine.AI.NavMeshAgent navMeshAgent;

    protected void Awake()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        worldStates = GWorld.Instance.GetWorldStates();
    }

    protected void Update()
    {
        SetGoalPriority();
        ExecutePlan();
        // ExecuteActionsPostEffects();
    }

    protected void ReorderGoals(string goalName, int index)
    {
        GGoalSO topGoal = null;

        foreach (GGoalSO goal in goals)
        {
            if (goal.GoalName.Contains(goalName))
            {
                topGoal = goal;
                break;
            }
        }
        if (topGoal != null)
        {
            goals.Remove(topGoal);
            goals.Insert(index, topGoal);
        }

    }

    protected void SetGoalPriority()
    {
        if (ratDetected)
        {
            ReorderGoals("ScareRat", 0);
            currentGoal = 1;
        }

        else if (patrolQuota <= 0.5)
        {
            if (energyLevel < bladderLevel && energyLevel < moraleLevel)
            {
                ReorderGoals("IncreaseEnergy", 0);
                currentGoal = 3;
            }
            else if (bladderLevel < energyLevel && bladderLevel < moraleLevel)
            {
                ReorderGoals("EmptyBladder", 0);
                currentGoal = 2;
            }
            else if (moraleLevel < energyLevel && moraleLevel < bladderLevel)
            {
                ReorderGoals("IncreaseMorale", 0);
                currentGoal = 4;
            }
            else
            {
                ReorderGoals("Patrol", 0);
                currentGoal = 0;
            }
        }
        else
        {
            ReorderGoals("Patrol", 0);
            currentGoal = 0;
        }

    }

    protected void SetWorldState(string state, bool value)
    {
        worldStates.SetWorldState(state, value);
    }

    // protected void ExecuteActionsPostEffects()
    // {
    //     Debug.Log($"State: KitchenVacant is {worldStates.GetWorldState("KitchenVacant")}");
    //     Debug.Log($"State: CoffeeAvailable is {worldStates.GetWorldState("CoffeeAvailable")}");
    //     Debug.Log($"State: BathroomVacant is {worldStates.GetWorldState("BathroomVacant")}");
    //     Debug.Log($"State: BunkVacant is {worldStates.GetWorldState("BunkVacant")}");

    //     foreach (var action in actions)
    //     {
    //         var postEffects = action.PostEffects;

    //         if (postEffects.Count > 0)
    //         {
    //             foreach (var effect in postEffects)
    //             {
    //                 SetWorldState(effect.state, effect.value);
    //                 Debug.Log($"State: {effect.state} is {worldStates.GetWorldState(effect.state)}");
    //             }
    //         }
    //     }
    // }

    protected void ExecutePlan()
    {
        navMeshAgent.destination = GPlanner.MakePlan(goals[0]);
    }

}
