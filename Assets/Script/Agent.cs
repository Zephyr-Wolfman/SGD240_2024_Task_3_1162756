using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class SubGoal
{
    public Dictionary<string, int> sGoals;
    public bool remove;

    public SubGoal(string s, int i, bool r)
    {
        sGoals = new Dictionary<string,int>();
        sGoals.Add(s,i);
        remove = r;
    }

}

public class Agent : MonoBehaviour
{
    public List<GoapAction> actions = new List<GoapAction>();
    public Dictionary<SubGoal, int> goals = new Dictionary<SubGoal, int>();

    Planner planner;
    Queue<GoapAction> actionQueue;
    public GoapAction currentAction;
    private SubGoal currentGoal;

    // Start is called before the first frame update
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
