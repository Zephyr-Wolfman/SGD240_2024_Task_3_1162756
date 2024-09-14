using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct Conditions
{
    public string state;
    public bool value;
}

[CreateAssetMenu]
public class GActionSO : ScriptableObject
{
    public string actionName;
    public float cost;

    public Vector3 nextlocation;
  
    [SerializeField]
    private List<Conditions> preCons = new List<Conditions>();
    [SerializeField]
    private List<Conditions> postEffects = new List<Conditions>();

    public List<Conditions> PostEffects
    {
       get { return PostEffects; }
    }
       
    public void SetAgentLevels()
    {
        
    }

    public void SetLocation(Vector3 loc)
    {
        nextlocation = loc;
    }

    
    

}
