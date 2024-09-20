using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// Defines an action an agent can take, including its properties, preconditions and post-effects.
/// Create the SO asset in the editor and adjust the fields to make a new action
/// </summary>
[CreateAssetMenu]
public class GActionSO : ScriptableObject
{
    // Fields for action properties
    [SerializeField]
    private string actionName;
    [SerializeField]
    private float cost;
    [SerializeField]
    private float actionDuration;
    [SerializeField]
    private GameObject nextLocation;    
    [SerializeField]
    private Conditions conditions;

    // Properties to access the fields
    public string ActionName => actionName;
    public float Cost => cost;
    public float ActionDuration => actionDuration;
    public Vector3 GetLocation() => nextLocation.transform.position;
    public Conditions PreConsPostFX => conditions;
}

/// <summary> 
/// Represents a condition that effects the states of the world and agent
/// </summary>
[System.Serializable]
public class Conditions
{
    [SerializeField]
    private List<StateValue> preCons = new List<StateValue>();
    [SerializeField]
    private List<StateValue> postEffects = new List<StateValue>();

    public float energyImpact;
    public float bladderImpact;
    public float moraleImpact;
    public float CoffeeSupplyImpact;
}

[System.Serializable]
public struct StateValue
{
    public string state;
    public bool value;
}

