// using System.Collections;
// using System.Collections.Generic;
// using UnityEngine;
// using System.Linq;
// using System;

// /// <summary> 
// /// Generates a sequence of actions that satisfies the agents goals.
// ///  It selects achievable actions and builds a path from the initial state to the goal
// /// </summary>
// public class Planner : MonoBehaviour
// {
//     // Generates a queue of actions that satisfies the agent's goal
//     public Queue<GoapAction> plan(List<GoapAction> actions, Dictionary<string, int> goal, WorldStates states)
//     {
//         List<GoapAction> usableActions = new List<GoapAction>();

//         // Filters actions to find ones that are achievable
//         foreach (GoapAction a in actions)
//         {
//             if (a.IsAchievable())
//             {
//                 usableActions.Add(a);
//             }
//         }

//         List<Node> leaves = new List<Node>();

//         // Create the root node with the initial state
//         Node start = new Node(null, 0, World.Instance.GetWorld().GetStates(), null);

//         // Attempt to build a valid grapgh of actions to achieve the goal
//         bool success = BuildGraph(start, leaves, usableActions, goal);

//         if (!success)
//         {
//             Debug.Log("No Plan");
//             return null;
//         }

//         // Find cheapest path
//         Node cheapest = null;
//         foreach (Node leaf in leaves)
//         {
//             if (cheapest == null)
//             {
//                 cheapest = leaf;
//             }
//             else
//             {
//                 if (leaf.cost < cheapest.cost)
//                 {
//                     cheapest = leaf;
//                 }
//             }
//         }

//         // Work backwards to build action sequence
//         List<GoapAction> result = new List<GoapAction>();
//         Node n = cheapest;
//         while (n != null)
//         {
//             if (n.action != null)
//             {
//                 result.Insert(0, n.action); // Insert action at the first index
//             }
//             n = n.parent;
//         }

//         // Convert the result list to a queue
//         Queue<GoapAction> queue = new Queue<GoapAction>();

//         foreach (GoapAction a in result)
//         {
//             queue.Enqueue(a);
//         }

//         return queue;
//     }

//     // Logic to build the action graph goes here
//     private bool BuildGraph(Node parent, List<Node> leaves, List<GoapAction> usableActions, Dictionary<string, int> goal)
//     {
//         return true;
//     }
// }

// /// <summary> 
// /// Nodes represent a state in the graph used by the planner.
// ///  Nodes hold a reference to the parent node, cost of reaching this state, the current world state, and the action that led to this state
// /// </summary>
// public class Node
// {
//     public Node parent;
//     public float cost;
//     public Dictionary<string, int> state;
//     public GoapAction action;

//     // Constructor for creating a new node
//     public Node(Node parent, float cost, Dictionary<string, int> allstates, GoapAction action)
//     {
//         this.parent = parent;
//         this.cost = cost;
//         this.state = new Dictionary<string, int>(allstates);
//         this.action = action;
//     }
// }
