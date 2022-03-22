using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToCamera : GAction
{
    public float CamRange;
    public int Damage = 10;

    public override bool PrePerform()
    {
        // display alert icon
        // animation change ??
        // speed change
        return true;
    }

    public override bool PostPerform()
    {
        // change or remove icon
        // animation change ??
        // speed change??
        beliefs.RemoveState("Alert");
        Debug.Log("Camera Search Done");
        return true;
    }


}
