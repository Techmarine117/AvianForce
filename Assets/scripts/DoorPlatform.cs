using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorPlatform : MonoBehaviour
{
    public Door door;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Player")
        {
            door.OpenDoorTimer();
        }
        
    }
}
