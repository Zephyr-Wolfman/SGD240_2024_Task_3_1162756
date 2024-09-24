using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GAgentStates : MonoBehaviour
{
    private Dictionary<string, bool> agentStates;

    private void Start()
    {
        InitAgentStates();
    }

    private void InitAgentStates()
    {
        agentStates = new Dictionary<string, bool>();
        agentStates.Add("InKitchen", false);
        agentStates.Add("HasCoffee", false);
        agentStates.Add("InBathroom", false);
        agentStates.Add("InBunkroom", false);
        agentStates.Add("RatDetected", false);        
        agentStates.Add("NearGuard", false);        
    }

    public void SetAgentState(string state, bool value)
    {
        if (agentStates.ContainsKey(state))
        {
            agentStates[state] = value;
        }
    }

    public bool GetAgentState(string state)
    {
        if (agentStates.ContainsKey(state))
        {
            return agentStates[state];
        }
        else
        {
            return false;
        }
    }
}
