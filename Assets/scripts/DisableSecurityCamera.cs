using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableSecurityCamera : MonoBehaviour
{
    [SerializeField]
    GameObject[] SecurityCams;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Players")
        {
            foreach (var item in SecurityCams)
            {
                Destroy(item);
            }
        }
    }

}
