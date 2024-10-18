using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// Base class for agents.
/// Manages agent levels, and goal planning
/// </summary>
public class GAgentBase : MonoBehaviour
{
    [SerializeField]
    protected int energyLevel = 10;
    [SerializeField]
    protected int bladderLevel = 10;
    [SerializeField]
    protected int moraleLevel = 10;
    [SerializeField]
    protected int patrolQuota = 10;
    [SerializeField]
    protected bool ratDetected = false;
    [SerializeField]
    protected int currentGoal = 0;
    protected bool planIsUpdated = true;
    protected bool postEffectsExecuted = false;
    [SerializeField]
    protected List<GGoalSO> goals = new List<GGoalSO>();
    [SerializeField]
    protected HashSet<GActionSO> achievableActions = new HashSet<GActionSO>();
    protected Rigidbody rb;
    protected GWorldStates worldStates;
    protected GAgentStates agentStates;
    protected UnityEngine.AI.NavMeshAgent navMeshAgent;

    // Initialises components, and world and agents states
    protected void Awake()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        worldStates = GWorldStates.Instance;
        agentStates = GetComponent<GAgentStates>();
        rb = GetComponent<Rigidbody>();
    }

    // Sets goal priotity and executes the plan
    protected void Update()
    {
        if (planIsUpdated)
        {
            SetGoalPriority();
            ExecutePlan();
            planIsUpdated = false;
        }
        // Debug.Log($"{goals[0].GoalName} is " + IsGoalAchievable(goals[0].GoalName));
    }

    // Reorders the goals
    protected void ReorderGoals(string goalName, int index)
    {
        GGoalSO topGoal = null;
        // planIsUpdated = true;

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
            achievableActions.Clear();
            // IsActionAchievable(goals[0].GoalName);
        }
    }

    // Set the goal priority based on current conditions
    protected void SetGoalPriority()
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

    // Creates and executes the plan for the agent based on current states
    protected void ExecutePlan()
    {
        List<StateValue> goalState = goals[0].DesiredStates;
        Dictionary<string, bool> worldState = worldStates.GetWorldStatesDict();
        Dictionary<string, bool> agentState = agentStates.GetAgentStatesDict();
        Queue<GActionSO> plan = GPlanner.Plan(new List<GActionSO>(goals[0].Actions), goalState, worldState, agentState);

        if (plan != null && plan.Count > 0)
        {
            postEffectsExecuted = false;
            StartCoroutine(ExecuteActionQueue(plan));
        }
        else
        {
            Debug.Log("No plan found.");
        }
    }

    // Executes the queue of actions and handles agent movement
    protected IEnumerator ExecuteActionQueue(Queue<GActionSO> actionQueue)
    {

        while (actionQueue.Count > 0)
        {
            GActionSO currentAction = actionQueue.Dequeue();

            Vector3 targetPosition = currentAction.GetLocation(this.gameObject);

            navMeshAgent.destination = targetPosition;

            Debug.Log("Agent is moving towards location");

            while (navMeshAgent.pathPending)
            {
                yield return null;
            }

            while (navMeshAgent.remainingDistance > navMeshAgent.stoppingDistance || navMeshAgent.velocity.sqrMagnitude > 0f)
            {
                yield return null;
            }
            Debug.Log("Agent has reached the destination");

            Debug.Log($"Waiting for {currentAction.ActionDuration} seconds for action: {currentAction.ActionName}");

            yield return new WaitForSeconds(currentAction.ActionDuration);
            // SetRoomStatus();
            ExecuteActionsPostEffects(currentAction.ActionName);
            agentStates.SetAgentState("Patrolling", false);
        }
        Debug.Log("All actions in the queue have been executed.");
    }

    // Applies the post effects of the current action to the world and agent states
    protected void ExecuteActionsPostEffects(string actionName)
    {
        if (!postEffectsExecuted)
        {
            foreach (GGoalSO goal in goals)
            {
                foreach (GActionSO action in goal.Actions)
                {
                    if (action.ActionName == actionName)
                    {
                        var postEffects = action.PreConsPostFX.PostEffects;

                        if (postEffects.Count > 0)
                        {
                            foreach (var effect in postEffects)
                            {
                                worldStates.SetWorldState(effect.state, effect.value);

                                Debug.Log($"State: {effect.state} is {worldStates.GetWorldState(effect.state)}");
                            }


                            foreach (var effect in postEffects)
                            {
                                agentStates.SetAgentState(effect.state, effect.value);

                                // Debug.Log($"State: {effect.state} is {agentStates.GetAgentState(effect.state)}");
                                Debug.Log($"Patrolling Agent State: {agentStates.GetAgentState("Patrolling")}");
                            }
                        }
                        UpdateAgentLevels(action);
                    }
                }
            }
        }
        postEffectsExecuted = true;
        Debug.Log("postEffectsExecuted = " + postEffectsExecuted);
        planIsUpdated = true;
    }

    // Updates the agent levels after each action is executed
    protected void UpdateAgentLevels(GActionSO action)
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

