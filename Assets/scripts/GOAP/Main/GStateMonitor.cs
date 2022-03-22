using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GStateMonitor : MonoBehaviour
{
    public string state, queueName , worldState;
    public float stateStrength, stateDecayRate , intialStrength;
    public WorldStates beliefs;
    public GameObject resourcePrefab;
    public GAction action;
    bool stateFound = false;

    
    void Awake()
    {
        beliefs = this.GetComponent<GAgent>().beliefs;
        intialStrength = stateStrength;
    }


    void LateUpdate()
    {
        if (action.running)
        {
            stateFound = false;
            stateStrength = intialStrength;
        }

        if(!stateFound && beliefs.HasState(state))
        {
            stateFound = true;
        }
            
        if (stateFound)
        {
            stateStrength -= stateDecayRate * Time.deltaTime;
            if(stateStrength <= 0)
            {
                Vector3 location = new Vector3(this.transform.position.x, resourcePrefab.transform.position.y , this.transform.position.z);
                GameObject p = Instantiate(resourcePrefab, location, resourcePrefab.transform.rotation);
                stateFound = false;
                stateStrength = intialStrength;
                beliefs.RemoveState(state);
                GWorld.Instance.GetQueue(queueName).AddResource(p);
                GWorld.Instance.GetWorld().ModifyState(worldState, 1);
            }
        }
    }
   
}
