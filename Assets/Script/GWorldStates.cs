using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GWorldStates : MonoBehaviour
{   
    private float coffeeLevel = 1;
    private Dictionary<string, bool> worldStates = new Dictionary<string, bool>();

    private void Update()
    {
        
    }

    public void WorldStates()
    {
        worldStates.Add("KitchenVacant", true);        
        worldStates.Add("CoffeeAvailable", true);
        worldStates.Add("BathroomVacant", true);
        worldStates.Add("BunkVacant", true);    
    }

    public void SetWorldState(string state, bool value)
    {
        if (worldStates.ContainsKey(state))
        {
            worldStates[state] = value;
        }
    }

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

    public float SetCoffeeLevel(float coffeeChange)
    {
        coffeeLevel += coffeeChange;
        return coffeeLevel;
    }


        
}
