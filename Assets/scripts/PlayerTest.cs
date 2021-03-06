using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerTest : MonoBehaviour
{
    public int maxHealth = 100, currentHealth;
    public Transform RespawnPosition;
    
    
    // Start is called before the first frame update
    void Start()
    {
        currentHealth = maxHealth; 
    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        // healthbar
        if (currentHealth <= 0)
        {
           Respawn(RespawnPosition);
        }
        Debug.Log("" + damage);
    }

    private void Respawn(Transform respawnPoint)
    {
        // Penalties ?
        // health Reset
        currentHealth = maxHealth;
        // Position Reset
        gameObject.transform.position = respawnPoint.position;

    }


  
}
