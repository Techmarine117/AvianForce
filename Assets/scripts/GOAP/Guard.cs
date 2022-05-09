using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Guard : GAgent
{
    public int enemyHealth = 100;
    // Start is called before the first frame update
   new public void Start()
    {
        base.Start();

        SubGoal s1 = new SubGoal("Chase", 1, false);
        goal.Add(s1, 1);
        SubGoal s2 = new SubGoal("Alert", 1, false);
        goal.Add(s2, 2);      
    }

    public void CameraAlert(string camTag)
    {
        beliefs.ModifyState("Alert", 0);
        Debug.Log("ALERTING");
        if (GetComponent<GoToCamera>())
        {
            GetComponent<GoToCamera>().targetTag = camTag;
        }
    }

    public void Healthcheck()
    {
        if (enemyHealth <= 0)
        {
            Destroy(gameObject);
            //score ++
        }
    }
}
