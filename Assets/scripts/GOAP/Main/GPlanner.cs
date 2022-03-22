using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Nodes
{
    public Nodes parent;
    public float cost;
    public Dictionary<string, int> state;
    public GAction action;

    public Nodes(Nodes parent, float cost, Dictionary<string, int> allstates, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allstates);
        this.action = action;

    }

    public Nodes(Nodes parent, float cost, Dictionary<string, int> allstates, Dictionary<string, int> beliefstates, GAction action)
    {
        this.parent = parent;
        this.cost = cost;
        this.state = new Dictionary<string, int>(allstates);
        foreach (KeyValuePair<string, int> b in beliefstates)
            if (!this.state.ContainsKey(b.Key))
                this.state.Add(b.Key, b.Value);
        this.action = action;

    }
}
//More conditions
//if action is chasing player, we dont want want to use the queue system.
//basically letting all the guards do specific actions and ignore the queue system.
public class GPlanner 
{
   public Queue<GAction> plan(List<GAction> actions, Dictionary<string, int> goal, WorldStates beliefstates)
    {
        List<GAction> usableActions = new List<GAction>();
        foreach(GAction a in actions)
        {
            if (a.IsAchievable())
                usableActions.Add(a);
        }
        List<Nodes> leaves = new List<Nodes>();
        Nodes start = new Nodes(null, 0, GWorld.Instance.GetWorld().GetStates(), beliefstates.GetStates(), null);

        bool success = BuildGraph(start, leaves, usableActions, goal);
        if (!success)
        {
            Debug.Log("No Plan");
            return null;
        }

        Nodes cheapest = null;
        foreach(Nodes leaf in leaves)
        {
            if (cheapest == null)
                cheapest = leaf;
            else
            {
                if (leaf.cost < cheapest.cost)
                    cheapest = leaf;
            }
        }
        List<GAction> result = new List<GAction>();
        Nodes n = cheapest;
        while(n != null)
        {
            if(n.action != null)
            {
                result.Insert(0, n.action);
            }
            n = n.parent;
        }
        Queue<GAction> queue = new Queue<GAction>();
        foreach(GAction a in result)
        {
            queue.Enqueue(a);
        }
        Debug.Log("The Plan is:");
        foreach(GAction a in queue)
        {
            Debug.Log("Q:" + a.actionName);
        }
        return queue;
    }
    private bool BuildGraph(Nodes parent, List<Nodes> leaves, List<GAction> usableActions, Dictionary<string, int> goal)
    {
        bool foundPath = false;
        foreach(GAction action in usableActions)
        {
            if (action.IsAchievableGiven(parent.state))
            {
                Dictionary<string, int> currentState = new Dictionary<string, int>(parent.state);
                foreach(KeyValuePair<string, int>eff in action.effects)
                {
                    if (!currentState.ContainsKey(eff.Key))
                        currentState.Add(eff.Key, eff.Value);
                }
                Nodes node = new Nodes(parent, parent.cost + action.cost, currentState, action);

                if(GoalAchieved(goal, currentState))
                {
                    leaves.Add(node);
                    foundPath = true;
                }
                else
                {
                    List<GAction> subset = ActionSubset(usableActions, action);
                    bool found = BuildGraph(node, leaves, subset, goal);
                    if (found)
                        foundPath = true;
                }
            }
        }
        return foundPath;
    }
    private bool GoalAchieved(Dictionary<string, int> goal, Dictionary<string, int> state)
    {
        foreach(KeyValuePair<string, int> g in goal)
        {
            if (!state.ContainsKey(g.Key))
                return false;
        }
        return true;
    }
    private List<GAction> ActionSubset(List<GAction> actions, GAction removeMe)
    {
        List<GAction> subset = new List<GAction>();
        foreach(GAction a in actions)
        {
            if (!a.Equals(removeMe))
                subset.Add(a);
        }
        return subset;
    }
}