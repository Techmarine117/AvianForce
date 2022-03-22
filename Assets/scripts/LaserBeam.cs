using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBeam : MonoBehaviour
{
    public int Damage = 10;
  

    private void OnTriggerEnter(Collider Col)
    {
        if (Col.gameObject.tag == "Player")
        {
            Col.gameObject.GetComponent<PlayerTest>().TakeDamage(Damage);
        }
    }
}
