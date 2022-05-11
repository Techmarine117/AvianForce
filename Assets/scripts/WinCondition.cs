using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WinCondition : MonoBehaviour
{
    public bool isPickedup;
    public GameObject Object;




    private void OnTriggerEnter(Collider other)
    {
       
        if (isPickedup == true)
        {
            if (other.tag == "Players")
            {
                Object.SetActive(true);

                // win
            }
        }
    }
}
