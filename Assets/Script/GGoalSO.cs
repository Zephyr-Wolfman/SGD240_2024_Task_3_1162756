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
    private GActionSO[] actions;
    
    // Properties to access the fields
    public string GoalName => goalName;
    public GActionSO[] Actions => actions; 

}

