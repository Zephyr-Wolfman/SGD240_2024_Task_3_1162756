using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/// <summary> 
/// Defines the structure for pre-conditions, post-effects, and execution of actions.
///  It's an abstract class and derived classes must implement methods
/// </summary>
public abstract class GoapAction : MonoBehaviour
{
    public string actionName = "Action";
    [SerializeField]
    private float cost = 1.0f;
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private GameObject targetTag;
    [SerializeField]
    private float duration = 0;
    [SerializeField]
    private WorldState[] preConditions;
    [SerializeField]
    private WorldState[] postEffects;
    [SerializeField]
    private NavMeshAgent agent;
    [SerializeField]
    private Dictionary<string, int> preConditionsDict;
    [SerializeField]
    private Dictionary<string, int> postEffectsDict;
    [SerializeField]
    private WorldStates agentBeliefs;
    [SerializeField]
    private bool actionInProgress = false;

    // Initialises the dictionaries for pre-conditions and post-effects
    public void Action()
    {
        preConditionsDict = new Dictionary<string, int>();
        postEffectsDict = new Dictionary<string, int>();
    }

    // Sets up the agent's NavMeshAGent and adds the pre-conditions and post-effects to dictionaries
    public void Awake()
    {
        agent = this.gameObject.GetComponent<NavMeshAgent>();

        if (preConditions != null)
        {
            foreach (WorldState w in preConditions)
            {
                preConditionsDict.Add(w.stateKey, w.stateValue);
            }
        }

        if (postEffects != null)
        {
            foreach (WorldState w in postEffects)
            {
                postEffectsDict.Add(w.stateKey, w.stateValue);
            }
        }

    }

    // Returns true if the action can be achieved
    public bool IsAchievable()
    {
        return true;
    }

    // Checks if the action can be achieved with the current world states
    public bool IsAchievableIf(Dictionary<string, int> conditions)
    {
        foreach (KeyValuePair<string, int> p in preConditionsDict)
        {
            if (!conditions.ContainsKey(p.Key))
            {
                return false;
            }
        }
        return true;
    }

    // Abstract method for pre-action must be implemented by derived classes
    public abstract bool PrePerform();
    // Abstract method for post-action must be implemented by derived classes
    public abstract bool PostPerform();
}
