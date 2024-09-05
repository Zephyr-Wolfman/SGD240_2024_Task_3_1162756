using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;


public class Node
{
    public Node parent;
    public float cost;
    public Dictionary<string, int> state;
    public GoapAction action;

    public Node(Node parent, float cost, Dictionary<string, int> allstates, GoapAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allstates);
        this.action = action;
    }
}

public class Planner : MonoBehaviour
{
    public Queue<GoapAction> plan(List<GoapAction> actions, Dictionary<string, int> goal, WorldStates states)
    {
        List<GoapAction> usableActions = new List<GoapAction>();
        foreach (GoapAction a in actions)
        {
            if (a.IsAchievable())
            {
                usableActions.Add(a);
            }
        }

        List<Node> leaves = new List<Node>();

        Node start = new Node(null, 0, World.Instance.GetWorld().GetStates(), null);

        bool success = BuildGraph(start, leaves, usableActions, goal);

        if (!success)
        {
            Debug.Log("No Plan");
            return null;
        }

        Node cheapest = null;

        foreach (Node leaf in leaves)
        {
            if (cheapest == null)
            {
                cheapest = leaf;
            }
            else
            {
                if (leaf.cost < cheapest.cost)
                {
                    cheapest = leaf;
                }
            }
        }

        List<GoapAction> result = new List<GoapAction>();
        Node n = cheapest;
        while (n != null)
        {
            if (n.action != null)
            {
                result.Insert(0, n.action);
            }
            n = n.parent;
        }

        Queue<GoapAction> queue = new Queue<GoapAction>();

        foreach (GoapAction a in result)
        {
            queue.Enqueue(a);
        }

        Debug.Log("The Plan is: ");
        foreach (GoapAction a in queue)
        {
            Debug.Log("Q: " + a.actionName);
        }

        return queue;
    }

    private bool BuildGraph(Node parent, List<Node> leaves, List<GoapAction> usableActions, Dictionary<string, int> goal)
    {
        return true;
    }
}
