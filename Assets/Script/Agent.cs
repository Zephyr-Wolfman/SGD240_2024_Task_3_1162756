using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

/// <summary> 
/// Manages available actions and goals.
/// Actions are performed based on the goals and the output of the planner
/// </summary>
public class Agent : MonoBehaviour
{
    public List<GoapAction> actions = new List<GoapAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();

    Planner planner;
    Queue<GoapAction> actionQueue;
    public GoapAction currentAction;
    private SubGoal currentGoal;

    // Gets all GoapAction components attached to the agent and adds available actions to the action list
    void Start()
    {
        GoapAction[] acts = this.GetComponents<GoapAction>();
        foreach (GoapAction a in acts)
        {
            actions.Add(a);
        }
    }

    // Update is called once per frame
    void LateUpdate()
    {
        
    }
}

/// <summary> 
/// Each sub-goal contains a dictionary with a key, a value, and a bool that determines removal
/// </summary>
public class SubGoal
{
    public Dictionary<string, int> sGoals;
    public bool remove;

    // Constructor to initialise sub-goal
    public SubGoal(string s, int i, bool r)
    {
        sGoals = new Dictionary<string,int>();
        sGoals.Add(s,i);
        remove = r;
    }

}

