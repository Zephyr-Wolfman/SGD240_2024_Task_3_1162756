using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// Manages the global states of the world, like room vacancy status and resources such as coffee level
/// </summary>
public class GWorldStates : MonoBehaviour
{
    private float coffeeLevel = 1;
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


    private void Start()
    {
        InitWorldStatesDict();
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

    // public access to update the world state
    public void SetWorldState(string state, bool value)
    {
        if (worldStates.ContainsKey(state))
        {
            worldStates[state] = value;
        }
        else
        {
            worldStates.Add(state, value);
        }
    }

    // public access to get the world state
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

    // public access to update the coffee levels
    public float SetCoffeeLevel(float coffeeChange)
    {
        coffeeLevel += coffeeChange;
        return coffeeLevel;
    }



}
