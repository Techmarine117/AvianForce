using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SecurityCamera : MonoBehaviour
{
    [SerializeField]
    private int CameraIndex = 0;
    bool GuardSpawned , PlayerDetected;
    [SerializeField]
    private float NumberOfGuardsToSpawn = 3;
    [SerializeField]
    private float GuardAlertRange = 3;
    [SerializeField]
    float timer;
    public float detectionTime = 5f;
    public GameObject AiPrefab;
    public Transform GuardSpawnPosition;


    private void Start()
    {
        gameObject.tag = "SecurityCam" + CameraIndex.ToString();
    }
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Players")
        {
            PlayerDetected = true;
            //flashing lights
        }
            
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Players")
        {
            PlayerDetected = false;
            timer = 0;
        }
    }

    void Update()
    {
        if (PlayerDetected == true)
        {
            timer += Time.deltaTime;
            if (timer >= detectionTime && GuardSpawned == false)
            {
                GuardSpawned = true;
                Collider[] colliders = Physics.OverlapSphere(transform.position, GuardAlertRange);
                int j = 0;
                foreach (var collider in colliders)
                {
                    if (collider.tag == "Guard")
                    {
                        j++;
                        collider.gameObject.GetComponent<Guard>().CameraAlert(gameObject.tag);
                    }
                }
                // if j is 0 then it means there had been no guards to alert
                if (j == 0)
                {
                    for (int i = 0; i < NumberOfGuardsToSpawn; i++)
                    {
                        Instantiate(AiPrefab, GuardSpawnPosition.position, Quaternion.identity);
                    }
                }
               
                //alert state where ai will move towards the camera position
            }
            Debug.Log(timer);
        }
    }
}
