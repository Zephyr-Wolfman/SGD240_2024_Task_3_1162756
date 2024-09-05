using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

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

    public void Action()
    {
        preConditionsDict = new Dictionary<string, int>();
        postEffectsDict = new Dictionary<string, int>();
    }

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

    public bool IsAchievable()
    {
        return true;
    }

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

    public abstract bool PrePerform();
    public abstract bool PostPerform();
}
