using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// Manages the global states of the world, like room vacancy status and resources such as coffee level.
/// Implements lazy instaniation of a singlton
/// </summary>
public class GWorldStates : MonoBehaviour
{
    private int coffeeLevel = 1;
    private Dictionary<string, bool> worldStates;

    private static GWorldStates instance;
    public static GWorldStates Instance
    {
        get
        {
            if (instance == null)
            {
                GameObject worldStatesObj = new GameObject("GWorldStatesObject");
                instance = worldStatesObj.AddComponent<GWorldStates>();
            }
            return instance;
        }
    }

    // Initialise world states dictionary
    private void Awake()
    {
        InitWorldStatesDict();
    }

    private void Update()
    {
        foreach (var state in worldStates)
        {
            // Debug.Log($"World state: {state.Key} is {state.Value}");
        }
    }

    // Initilises the world states dictionary and adds initial the defalt states
    public void InitWorldStatesDict()
    {
        worldStates = new Dictionary<string, bool>();

        worldStates.Add("KitchenVacant", true);
        worldStates.Add("CoffeeAvailable", true);
        worldStates.Add("BathroomVacant", true);
        worldStates.Add("BunkVacant", true);
    }

    // Sets the value of a world state
    public void SetWorldState(string state, bool value)
    {
        if (worldStates.ContainsKey(state))
        {
            worldStates[state] = value;
        }
       
    }

    // Returns the value of a world state
    public bool GetWorldState(string state)
    {
        if (worldStates.ContainsKey(state))
        {
            return worldStates[state];
        }
        else
        {
            return false;
        }
    }

    // Returns a copy of the world states dictionary
    public Dictionary<string, bool> GetWorldStatesDict()
    {
        return new Dictionary<string, bool>(worldStates);
    }

    // Updates the coffee supply levels
    public int SetCoffeeLevel(int coffeeChange)
    {
        coffeeLevel += coffeeChange;
        return coffeeLevel;
    }
}
