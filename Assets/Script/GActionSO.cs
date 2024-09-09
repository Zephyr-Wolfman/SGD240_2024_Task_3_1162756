using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Conditions
{
    public string state;
    public bool value;
}

[CreateAssetMenu]
public class GActionSO : ScriptableObject
{
    public string actionName;
    public float cost;

    public Vector3 location;

    [SerializeField]
    private float energyLevel = 1f;
    [SerializeField]
    private float bladderLevel = 0f;
    [SerializeField]
    private float moraleLevel = 1f;

    [SerializeField]
    private List<Conditions> PreCons = new List<Conditions>();
    [SerializeField]
    private List<Conditions> PostEffects = new List<Conditions>();
    
    public void EffectAgentLevels()
    {
        
    }

    public void SetLocation(Vector3 loc)
    {
        location = loc;
    }
    

}
