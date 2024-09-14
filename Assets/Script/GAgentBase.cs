using System.Collections;
using System.Collections.Generic;
using System.Globalization;
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
    protected float patrolQuota = 1f;
    [SerializeField]
    protected bool ratDetected = false;

    [SerializeField]
    protected GActionSO[] actions;
    protected List<string> goals = new List<string>();

    protected GWorldStates worldStates;

    protected UnityEngine.AI.NavMeshAgent navMeshAgent;

    protected void Awake()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        worldStates = FindObjectOfType<GWorldStates>();

        goals.Add("Patrol");
        goals.Add("EmptyBladder");
        goals.Add("IncreaseEnergy");
        goals.Add("IncreaseMorale");
        goals.Add("ChaseRat");
    }

    protected void Update()
    {
        SetGoalPriority();
        Debug.Log($"{goals[0]}");
        Debug.Log($"{worldStates.GetWorldState("KitchenVacant")}");
        SetWorldState(actions[0].PostEffects[0].state, actions[0].PostEffects[0].value);
        Debug.Log($"{worldStates.GetWorldState("KitchenVacant")}");

    }

    protected void ReorderGoals(string goal, int index)
    {
        if (goals.Contains(goal))
        {
            goals.Remove(goal);
            goals.Insert(index, goal);
        }
    }

    protected void SetGoalPriority()
    {
        if (ratDetected)
        {
            ReorderGoals("ChaseRat", 0);
        }

        else if (patrolQuota <= 0.5)
        {
            if (energyLevel < bladderLevel && energyLevel < moraleLevel)
            {
                ReorderGoals("IncreaseEnergy", 0);
            }
            else if (bladderLevel < energyLevel && bladderLevel < moraleLevel)
            {
                ReorderGoals("EmptyBladder", 0);
            }
            else if (moraleLevel < energyLevel && moraleLevel < bladderLevel)
            {
                ReorderGoals("IncreaseMorale", 0);
            }
            else
            {
                ReorderGoals("Patrol", 0);
            }
        }
        else
        {
            ReorderGoals("Patrol", 0);
        }

    }

    protected void SetWorldState(string state, bool value)
    {
        worldStates.SetWorldState(state, value);
    }








}
