using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class GGoalSO : ScriptableObject
{
    // Fields for action properties
    [SerializeField]
    private string goalName;
    [SerializeField]
    private int priority;
    [SerializeField]
    private GActionSO[] actions;
    [SerializeField]
    private List<StateValue> desiredStates;
    
    // Properties to access the fields
    public string GoalName => goalName;
    public int Priority => priority;    
    public GActionSO[] Actions => actions;
    public List<StateValue> DesiredStates => desiredStates; 

}

