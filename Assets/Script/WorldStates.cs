using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

// WorldStates holds the actual states

/// <summary> 
/// Represents a single world state with a key-value pair. 
///  Defines the conditions that affect agent behaviour.
///  Marked as [System.Serializable] so instances can be serialized and displayed in the editor 
/// </summary>
[System.Serializable]
public class WorldState
{
    public string stateKey;
    public int stateValue;  
}

/// <summary> 
/// Manages a collection of world states in a dictionary.
///  Contains methods for adding and modifying states that agents use to evaluate conditions in the world
/// </summary>
public class WorldStates
{
    private Dictionary<string, int> states;

    // Constructor to initialise the the states dictionary
    public WorldStates()
    {
        states = new Dictionary<string, int>();
    }

    // Check if state with a specific key exists
    public bool HasState(string key)
    {
        return states.ContainsKey(key);
    }

    // Add new state to dictionary
    public void AddState(string key, int value)
    {
        states.Add(key, value);
    }

    // Modify a states value, or remove it if its key index drops to 0 or less
    public void ModifyState(string key, int value)
    {
        if (states.ContainsKey(key))
        {
            states[key] += value;

            if (states[key] <= 0)
            {
                RemoveState(key);
            }
            else
            {
                states.Add(key, value);
            }

        }
    }

    // Remove a state from the dictionary
    public void RemoveState(string key)
    {
        if (states.ContainsKey(key))
        {
            states.Remove(key);
        }
    }
    
    // Set a state to a specific value, or add it if it does not exist
    public void SetState(string key, int value)
    {
        if (states.ContainsKey(key))
        {
            states[key] = value;
        }
        else
        {
            states.Add(key, value);
        }
    }

    // Get the dictionary of all currrent states
    public Dictionary<string, int> GetStates()
    {
        return states;
    }
}
