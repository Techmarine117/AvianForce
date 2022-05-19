using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : GAction
{
    public float attackRange;
    public int Damage = 10;

    public override bool PrePerform()
    {
        Debug.Log("ATTTTAAACKK");
        return true;
    }

    public override bool PostPerform()
    {
       if(Vector3.Distance(target.transform.position,gameObject.transform.position) <= attackRange)
        {
            target.GetComponent<PlayerController>().TakeDamage(Damage);
        }
        return true;
    }


    
}
