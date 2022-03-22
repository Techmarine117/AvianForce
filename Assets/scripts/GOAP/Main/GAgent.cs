using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class SubGoal
{
    public Dictionary<string, int> sgoals;
    public bool remove;

    public SubGoal(string s, int i, bool r)
    {
        sgoals = new Dictionary<string, int>();
        sgoals.Add(s, i);
        remove = r;
    }
}

public class GAgent : MonoBehaviour
{
    public List<GAction> actions = new List<GAction>();
    public Dictionary<SubGoal, int> goal = new Dictionary<SubGoal, int>();
    public Ginventory inventory = new Ginventory();
    public WorldStates beliefs = new WorldStates();
    GPlanner planner;
    Queue<GAction> actionQueue;
    public GAction currentAction;
    SubGoal currentGoal;
    Vector3 destination = Vector3.zero;
    bool invoked = false;

    Rigidbody rb;
    public float speed = 5f;

    public Astar astar;
    public GridManager grid;
    WaypointAI waypointAI;
    AiPathHolder aiPath;

    // Start is called before the first frame update
    public void Start()
    {
        waypointAI = GetComponent<WaypointAI>();
        aiPath = GetComponent<AiPathHolder>();
        astar = GameObject.FindObjectOfType<Astar>();
        grid = GameObject.FindObjectOfType<GridManager>();
        rb = GetComponent<Rigidbody>();

        GAction[] acts = this.GetComponents<GAction>();
        foreach (GAction a in acts)
            actions.Add(a);
    }

    void CompleteAction()
    {
        currentAction.running = false;
        currentAction.PostPerform();
        invoked = false;
    }
    void LateUpdate()
    {
        if (currentAction != null && currentAction.running)
        {
            float distanceToTarget = Vector3.Distance(destination, this.transform.position);
            if (currentAction.actionName == "ChaseAction")
            {
                destination = currentAction.target.transform.position;
                astar.FindPath(currentAction.transform.position, destination, currentAction.gameObject);
                waypointAI.MoveTowards(aiPath.shortestPath, rb, speed, 1.5f);
            }
            if (distanceToTarget < 3f)
            {
                if (!invoked)
                {
                    Invoke("CompleteAction", currentAction.duration);
                    invoked = true;
                }
            }
            return;
        }
        if (planner == null || actionQueue == null)
        {
            planner = new GPlanner();

            var sortedGoals = from entry in goal orderby entry.Value descending select entry;
            foreach (KeyValuePair<SubGoal, int> sg in sortedGoals)
            {
                actionQueue = planner.plan(actions, sg.Key.sgoals, beliefs);
                if (actionQueue != null)
                {
                    currentGoal = sg.Key;
                    break;
                }
            }
        }
        if (actionQueue != null && actionQueue.Count == 0)
        {
            if (currentGoal.remove)
            {
                goal.Remove(currentGoal);
            }
            planner = null;
        }
        if (actionQueue != null && actionQueue.Count > 0)
        {
            currentAction = actionQueue.Dequeue();
            if (currentAction.PrePerform())
            {
                if (currentAction.target == null && currentAction.targetTag != "")
                {
                    GameObject[] targets = GameObject.FindGameObjectsWithTag(currentAction.targetTag);
                    currentAction.target = targets[Random.Range(0,targets.Length)];
                }

                if (currentAction.target != null)
                {
                    currentAction.running = true;
                    destination = currentAction.target.transform.position;
                    Transform dest = currentAction.target.transform.Find("Destination");
                    if (dest != null)
                    {
                        destination = dest.position;
                    }
                    //currentAction.agent.SetDestination(destination);
                    astar.FindPath(currentAction.transform.position, destination, currentAction.gameObject);
                    waypointAI.MoveTowards(aiPath.shortestPath, rb, speed, 1.5f);

                }
            }
            else
            {
                actionQueue = null;
            }
        }
    }
}
