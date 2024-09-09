using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GAgentBase : MonoBehaviour
{
    [SerializeField]
    protected float energyLevel = 1f;
    [SerializeField]
    protected float maxEnergyLevel = 1f;
    [SerializeField]
    protected float bladderLevel = 0f;
    [SerializeField]
    protected float maxBladderLevel = 1f;
    [SerializeField]
    protected float moraleLevel = 1f;

    [SerializeField]
    protected GActionSO[] actions;

    protected UnityEngine.AI.NavMeshAgent navMeshAgent;

    protected void Awake()
    {
        navMeshAgent = GetComponent<UnityEngine.AI.NavMeshAgent>();

    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
