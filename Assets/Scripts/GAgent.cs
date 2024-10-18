using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAgent : GAgentBase
{
    [SerializeField]
    private int energyLevel = 10;
    [SerializeField]
    private int bladderLevel = 10;
    [SerializeField]
    private int moraleLevel = 10;
    [SerializeField]
    private int patrolQuota = 10;
    [SerializeField]
    private bool ratDetected = false;

    // Set the goal priority based on current conditions
    protected override void SetGoalPriority()
    {
        if (ratDetected)
        {
            ReorderGoals("ScareRat", 0);
            currentGoal = 1;
        }

        else if (patrolQuota <= 10)
        {
            if (energyLevel < 5 && energyLevel < bladderLevel && energyLevel < moraleLevel)
            {
                ReorderGoals("IncreaseEnergy", 0);
                currentGoal = 3;
            }
            else if (bladderLevel < 5 && bladderLevel < energyLevel && bladderLevel < moraleLevel)
            {
                ReorderGoals("EmptyBladder", 0);
                currentGoal = 2;
            }
            else if (moraleLevel < 5 && moraleLevel < energyLevel && moraleLevel < bladderLevel)
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


    // Updates the agent levels after each action is executed
    protected override void UpdateAgentLevels(GActionSO action)
    {
        energyLevel += action.PreConsPostFX.energyImpact;
        energyLevel = Mathf.Clamp(energyLevel, 0, 10);
        bladderLevel += action.PreConsPostFX.bladderImpact;
        bladderLevel = Mathf.Clamp(bladderLevel, 0, 10);
        moraleLevel += action.PreConsPostFX.moraleImpact;
        moraleLevel = Mathf.Clamp(moraleLevel, 0, 10);
        patrolQuota += action.PreConsPostFX.PatrolQuotaImpact;
        patrolQuota = Mathf.Clamp(patrolQuota, 0, 10);
    }
  
}
