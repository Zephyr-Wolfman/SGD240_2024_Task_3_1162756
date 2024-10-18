using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// Manages the idividual agent states using a dictionary of state flags.
/// Attach this script to all agent prefabs
/// </summary>
public class GAgentStates : MonoBehaviour
{
    private Dictionary<string, bool> agentStates;

    // Initialise agent states dictionary
    private void Awake()
    {
        InitAgentStates();
    }

    // Setup states for agent
    private void InitAgentStates()
    {
        agentStates = new Dictionary<string, bool>();
        agentStates.Add("InKitchen", false);
        agentStates.Add("HasCoffee", false);
        agentStates.Add("InBathroom", false);
        agentStates.Add("InBunkroom", false);
        agentStates.Add("RatDetected", false);        
        agentStates.Add("NearGuard", false);        
        agentStates.Add("CanPatrol", true);        
        agentStates.Add("Patrolling", false);        
        agentStates.Add("EnergyIncreased", false);        
        agentStates.Add("BladderEmpty", false);        
        agentStates.Add("HandsClean", false);        
    }

    // Updates the value of an agent state
    public void SetAgentState(string state, bool value)
    {
        if (agentStates.ContainsKey(state))
        {
            agentStates[state] = value;
        }
    }

    // Returns the value of an agent state
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

    // Returns a copy of the agent states dictionary
    public Dictionary<string, bool> GetAgentStatesDict()
    {
        return new Dictionary<string, bool>(agentStates);
    }
}
