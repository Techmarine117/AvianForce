using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Teleport : MonoBehaviour
{
    //empty gameobject
    [SerializeField]
    Transform TeleportPosition;
    // Start is called before the first frame update
    void Start()
    {
        // reference
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Players" && other.GetComponent<PlayerController>().isVulture == false)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                TeleportBlueBird(other.gameObject);
            }
        }
    }
    void TeleportBlueBird(GameObject bird)
    {
        bird.transform.position = TeleportPosition.position;
    }
}
