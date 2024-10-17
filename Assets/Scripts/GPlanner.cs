using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> 
/// Searches for and builds a sequence of actions to fulfill a goal.
/// </summary>
public static class GPlanner
{
    // Node is a single state in the planning tree
    public class Node
    {
        public Node Parent;
        public float Cost;
        public Dictionary<string, bool> WorldState;
        public Dictionary<string, bool> AgentState;
        public GActionSO Action;

        // Constructor for Node
        public Node(Node parent, float cost, Dictionary<string, bool> worldState, Dictionary<string, bool> agentState, GActionSO action)
        {
            Parent = parent;
            Cost = cost;
            WorldState = new Dictionary<string, bool>(worldState);
            AgentState = new Dictionary<string, bool>(agentState);
            Action = action;
        }
    }

    // Creates a plan to achieve the goal from the world and agent states
    // Used Depth-First Search Algorithm to find a sequence of actions
    public static Queue<GActionSO> Plan(List<GActionSO> availableActions, List<StateValue> goalState, Dictionary<string, bool> worldState, Dictionary<string, bool> agentState)
    {
        Node startNode = new Node(null, 0, worldState, agentState, null);
        HashSet<Node> closedList = new HashSet<Node>(new NodeEqualityComparer());
        Node goalNode = FindPath(startNode, new HashSet<GActionSO>(availableActions), goalState, closedList);

        if (goalNode != null)
        {
            return BuildActionQueue(goalNode);
        }
        else
        {
            return null;
        }
    }

    // Recursive search for a sequence of actions
    private static Node FindPath(Node currentNode, HashSet<GActionSO> actions, List<StateValue> goalState, HashSet<Node> closedList)
    {
        if (GoalAchieved(currentNode, goalState))
        {
            return currentNode;
        }

        closedList.Add(currentNode);

        foreach (var action in actions)
        {
            if (ActionUsable(action, currentNode))
            {
                Node newNode = CreateNewNode(action, currentNode);

                if (!NodeInList(newNode, closedList))
                {
                    Node result = FindPath(newNode, actions, goalState, closedList);

                    if (result != null)
                    {
                        return result;
                    }
                }
            }
        }
        return null;
    }

    // Checks if the current node satifies the goal conditions
    private static bool GoalAchieved(Node node, List<StateValue> goalState)
    {
        foreach (var goal in goalState)
        {
            bool worldValue = false;
            bool agentValue = false;

            node.WorldState.TryGetValue(goal.state, out worldValue);
            node.AgentState.TryGetValue(goal.state, out agentValue);

            if (worldValue != goal.value && agentValue != goal.value)
            {
                Debug.Log($"Goal state '{goal.state}' is not achieved. WorldState: {worldValue}, AgentState: {agentValue}, DesiredGoal: {goal.value}");
                return false;
            }
        }
        return true;
    }

    // Checks if an action can be used with the current node's state
    private static bool ActionUsable(GActionSO action, Node node)
    {
        foreach (var preCon in action.PreConsPostFX.PreCons)
        {
            bool worldValue = false;
            bool agentValue = false;
            node.WorldState.TryGetValue(preCon.state, out worldValue);
            node.AgentState.TryGetValue(preCon.state, out agentValue);

            if (worldValue != preCon.value && agentValue != preCon.value)
            {
                Debug.Log($"Action '{action.ActionName}' is not usable because '{preCon.state}' is not met");
                return false;
            }
        }
        return true;
    }

    // Creates a new node by applying the action's effects to current node's state
    private static Node CreateNewNode(GActionSO action, Node parent)
    {
        var newWorldState = new Dictionary<string, bool>(parent.WorldState);
        var newAgentState = new Dictionary<string, bool>(parent.AgentState);

        foreach (var effect in action.PreConsPostFX.PostEffects)
        {
            if (newWorldState.ContainsKey(effect.state))
            {
                newWorldState[effect.state] = effect.value;
            }
            else if (newAgentState.ContainsKey(effect.state))
            {
                newAgentState[effect.state] = effect.value;
            }
            else
            {
                newAgentState.Add(effect.state, effect.value);
            }
        }
        Debug.Log($"Create new node with action '{action.ActionName}'");
        return new Node(parent, parent.Cost + action.Cost, newWorldState, newAgentState, action);
    }

    // Checks if node is already in the list
    private static bool NodeInList(Node node, HashSet<Node> nodelist)
    {
        return nodelist.Contains(node);
    }

    // Compares two node's world and agent states
    public class NodeEqualityComparer : IEqualityComparer<Node>
    {
        public bool Equals(Node nodeA, Node nodeB)
        {
            return StateEqual(nodeA.WorldState, nodeB.WorldState) && StateEqual(nodeA.AgentState, nodeB.AgentState);
        }

        // Creates a hash code fro the node by combining world and agent state hash codes
        // Uses prime numbers to minimise comparison collision
        public int GetHashCode(Node obj)
        {
            int hash = 17;

            foreach (var kvp in obj.WorldState)
            {
                hash = hash * 31 + kvp.Key.GetHashCode() + kvp.Value.GetHashCode();
            }

            foreach (var kvp in obj.AgentState)
            {
                hash = hash * 31 + kvp.Key.GetHashCode() + kvp.Value.GetHashCode();
            }
            return hash;
        }

        // Compares two dictionaries to check if they are equal
        private bool StateEqual(Dictionary<string, bool> dictA, Dictionary<string, bool> dictB)
        {
            if (dictA.Count != dictB.Count)
            {
                return false;
            }

            foreach (var kvp in dictA)
            {
                if (!dictB.TryGetValue(kvp.Key, out bool result) || result != kvp.Value)
                {
                    return false;
                }
            }
            return true;
        }
    }

    // Builds a queue of actions from the goal node by walking back to the root
    private static Queue<GActionSO> BuildActionQueue(Node goalNode)
    {
        Stack<GActionSO> actionStack = new Stack<GActionSO>();
        Node currentNode = goalNode;

        while (currentNode != null && currentNode.Action != null)
        {
            actionStack.Push(currentNode.Action);
            currentNode = currentNode.Parent;
        }
        return new Queue<GActionSO>(actionStack);
    }
}

