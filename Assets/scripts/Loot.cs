using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Loot : MonoBehaviour
{
    public WinCondition win;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.collider.tag == "Players")
        {
            win.isPickedup = true;
            Destroy(gameObject);
        }

    }
}
