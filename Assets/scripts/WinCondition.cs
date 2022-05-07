using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public bool isPickedup;

    void Start()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (isPickedup)
        {
            if (other.tag == "Players")
            {
                // win
            }
        }
    }
}
