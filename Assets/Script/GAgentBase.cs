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
    protected bool planIsUpdated = true;

    [SerializeField]
    protected List<GGoalSO> goals = new List<GGoalSO>();
    [SerializeField]
    protected List<GActionSO> achievalbleActions = new List<GActionSO>();
    // protected List<string> achievalbleActions = new List<string>();

    protected Rigidbody rb;

    protected GWorldStates worldStates;
    protected GAgentStates agentStates;

    protected UnityEngine.AI.NavMeshAgent navMeshAgent;

    protected void Awake()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();
        worldStates = GWorldStates.Instance;
        agentStates = GetComponent<GAgentStates>();
        rb = GetComponent<Rigidbody>();
    }

    protected void Update()
    {
        if (planIsUpdated)
        {
            
            ExecutePlan();
            planIsUpdated = false;
            SetGoalPriority();
        }

        // Debug.Log($"{goals[0].GoalName} is " + IsGoalAchievable(goals[0].GoalName));

        // ExecuteActionsPostEffects();
    }

    protected void ReorderGoals(string goalName, int index)
    {
        GGoalSO topGoal = null;
        planIsUpdated = true;

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
            achievalbleActions.Clear();
            IsActionAchievable(goals[0].GoalName);
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

    // protected void SetWorldState(string state, bool value)
    // {
    //     worldStates.SetWorldState(state, value);
    // }

    protected void ExecuteActionsPostEffects(string actionName)
    {
        foreach (GGoalSO goal in goals)
        {
            foreach (GActionSO action in goal.Actions)
            {
                if (action.ActionName.Contains(actionName))
                {
                    var postEffects = action.PreConsPostFX.PostEffects;

                    if (postEffects.Count > 0)
                    {
                        foreach (var effect in postEffects)
                        {
                            worldStates.SetWorldState(effect.state, effect.value);

                            Debug.Log($"State: {effect.state} is {worldStates.GetWorldState(effect.state)}");
                        }
                        energyLevel += action.PreConsPostFX.energyImpact;
                        bladderLevel += action.PreConsPostFX.bladderImpact;
                        moraleLevel += action.PreConsPostFX.moraleImpact;

                        foreach (var effect in postEffects)
                        {
                            agentStates.SetAgentState(effect.state, effect.value);

                            Debug.Log($"State: {effect.state} is {agentStates.GetAgentState(effect.state)}");
                        }

                    }
                }
            }
        }


        // foreach (GGoalSO goal in goals)
        // {

        // Debug.Log($"State: KitchenVacant is {worldStates.GetWorldState("KitchenVacant")}");
        // Debug.Log($"State: CoffeeAvailable is {worldStates.GetWorldState("CoffeeAvailable")}");
        // Debug.Log($"State: BathroomVacant is {worldStates.GetWorldState("BathroomVacant")}");
        // Debug.Log($"State: BunkVacant is {worldStates.GetWorldState("BunkVacant")}");
        // Debug.Log($"State: InKitchen is {agentStates.GetAgentState("InKitchen")}");
        // Debug.Log($"State: HasCoffee is {agentStates.GetAgentState("HasCoffee")}");
        // Debug.Log($"State: InBathroom is {agentStates.GetAgentState("InBathroom")}");
        // Debug.Log($"State: InBunkroom is {agentStates.GetAgentState("InBunkroom")}");
        // Debug.Log($"State: RatDetected is {agentStates.GetAgentState("RatDetected")}");
        // Debug.Log($"State: NearGuard is {agentStates.GetAgentState("NearGuard")}");

        //     foreach (GActionSO action in goal.Actions)
        //     {
        //         var postEffects = action.PreConsPostFX.PostEffects;

        //         if (postEffects.Count > 0)
        //         {
        //             foreach (var effect in postEffects)
        //             {
        //                 worldStates.SetWorldState(effect.state, effect.value);

        //                 // Debug.Log($"State: {effect.state} is {worldStates.GetWorldState(effect.state)}");
        //             }
        //             energyLevel += action.PreConsPostFX.energyImpact;
        //             bladderLevel += action.PreConsPostFX.bladderImpact;
        //             moraleLevel += action.PreConsPostFX.moraleImpact;

        //             foreach (var effect in postEffects)
        //             {
        //                 agentStates.SetAgentState(effect.state, effect.value);

        //                 Debug.Log($"State: {effect.state} is {agentStates.GetAgentState(effect.state)}");
        //             }

        //         }
        //     }

        // }
    }

    protected void ExecutePlan()
    {
        // navMeshAgent.destination = GPlanner.MakePlan(goals[0]);
        Queue<GPlanner.PlannedAction> actionQueue = GPlanner.MakePlan(achievalbleActions);

        if (actionQueue.Count > 0)
        {
            StartCoroutine(ExecuteActionQueue(actionQueue));
        }
    }

    protected IEnumerator ExecuteActionQueue(Queue<GPlanner.PlannedAction> actionQueue)
    {
        while (actionQueue.Count > 0)
        {
            GPlanner.PlannedAction currentAction = actionQueue.Dequeue();
            navMeshAgent.destination = currentAction.targetPosition;
            Debug.Log($"Agent is moving towards location");

            while (navMeshAgent.remainingDistance > 0.1f && !navMeshAgent.pathPending)
            {
                yield return null;
            }
            Debug.Log($"Agent has reached the destination");

            Debug.Log($"Waiting for {currentAction.duration} seconds for action: {currentAction.actionName}");

            yield return new WaitForSeconds(currentAction.duration);
            ExecuteActionsPostEffects(currentAction.actionName);

        }
        Debug.Log("All actions in the queue have been executed.");
        navMeshAgent.destination = Vector3.zero;
    }

    protected void IsActionAchievable(string goalName)
    {
        foreach (GGoalSO goal in goals)
        {
            if (goal.GoalName.Contains(goalName))
            {
                foreach (GActionSO action in goal.Actions)
                {
                    foreach (StateValue preCon in action.PreConsPostFX.PreCons)
                    {
                        if (worldStates.GetWorldState(preCon.state) == preCon.value)
                        {
                            if (!achievalbleActions.Contains(action))
                                achievalbleActions.Add(action);
                        }
                        if (agentStates.GetAgentState(preCon.state) != preCon.value)
                        {
                            if (!achievalbleActions.Contains(action))
                                achievalbleActions.Add(action);
                        }
                    }
                }

            }
        }


    }
    public struct AgentchievableActions
    {
        public string actionName;
        public float duration;
    }
}

