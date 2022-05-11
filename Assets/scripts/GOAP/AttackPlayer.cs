using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackPlayer : GAction
{
    public float attackRange;
    public int Damage = 10;

    public override bool PrePerform()
    {
        return true;
    }

    public override bool PostPerform()
    {
       if(Vector3.Distance(target.transform.position,gameObject.transform.position) <= attackRange)
        {
            target.GetComponent<PlayerTest>().TakeDamage(Damage);
        }
        return true;
    }


    
}
