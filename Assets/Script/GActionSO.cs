using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// Defines an action an agent can take, including it's properties, preconditions and post-effects.
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
    private List<Conditions> preCons = new List<Conditions>();
    [SerializeField]
    private List<Conditions> postEffects = new List<Conditions>();

    // Properties to access the fields
    public string ActionName => actionName;
    public float Cost => cost;
    public float ActionDuration => actionDuration;
    public Vector3 GetLocation() => nextLocation.transform.position;
    public List<Conditions> PreCons => preCons;
    public List<Conditions> PostEffects => postEffects;
}

/// <summary> 
/// Represents a condition that effects the states of the world and agent
/// </summary>
[System.Serializable]
public struct Conditions
{
    public string state;
    public bool value;
    public float energyImpact;
    public float bladderImpact;
    public float moraleImpact;
    public float CoffeeSupplyImpact;
}

