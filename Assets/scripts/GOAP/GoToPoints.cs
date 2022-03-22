using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToPoints : GAction
{
    public override bool PrePerform()
    {
        target = GWorld.Instance.GetQueue("Pins").RemoveResource();
        if (target == null)
            return false;

        inventory.AddItem(target);
        GWorld.Instance.GetWorld().ModifyState("FreePins", -1);
        return true;
    }

    public override bool PostPerform()
    {
        GWorld.Instance.GetQueue("Pins").AddResource(target);
        inventory.RemoveItem(target);
        GWorld.Instance.GetWorld().ModifyState("FreePins", 1);
        return true;
    }
}
