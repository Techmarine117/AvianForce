using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Cinemachine;
using UnityEngine.UI;

public class PlayerControllerOffline : MonoBehaviour
{

    public int maxHealth = 100, currentHealth;
    [SerializeField]
    public Transform RespawnPosition;
    [SerializeField]
    private Slider hbs;


    public MovementBehaviour movement;
    public AnimationBehaviour movementAnimation;
    public PlayerInput playerInput;
    public Guard guard;
    public float moventSmoothingSpeed = 1f;
    private Vector3 rawInputMovement;
    private Vector3 smoothInput;
    


    private GameObject vcam;

    public bool isVulture;

    private string currentControlScheme;

    private void Start()
    {
        movementAnimation.setupBehaviour();
       
        vcam = GameObject.FindGameObjectWithTag("vCam");

        currentHealth = maxHealth;
        hbs.value = currentHealth / maxHealth;
    }

    public void OnAttack(InputAction.CallbackContext Value)
    {
        if (Value.started)
        {
            movementAnimation.PlayerAttackAnimation();
            Damage();
        }
    }

    

    public void OnMove(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        
    }

    private void Update()
    {
        if (true)//networkingScript.GetPlayerID() == nwc.OwnerID)
        {
            if (vcam.GetComponent<CinemachineVirtualCamera>().Follow == null && vcam.GetComponent<CinemachineVirtualCamera>())
            {
                vcam.GetComponent<CinemachineVirtualCamera>().Follow = transform;
                vcam.GetComponent<CinemachineVirtualCamera>().LookAt = transform;
            }
            CalculateMovementInputSmoothing();
            UpdatePlayerMovement();
            UpdatePlayerAnimationMovement();
        }
    }

    void CalculateMovementInputSmoothing()
    {
        smoothInput = Vector3.Lerp(smoothInput, rawInputMovement, Time.deltaTime * moventSmoothingSpeed);

    }

    void UpdatePlayerMovement()
    {
        movement.UpdateMoveentData(smoothInput);
    }

    public PlayerInput GetActionAsset()
    {
        return playerInput;
    }

    void UpdatePlayerAnimationMovement()
    {
        movementAnimation.UpdateMovementAnimation(smoothInput.magnitude);
    }

    void Damage()
    {
        if (isVulture)
        {
            // set the enemy health to 0%
            guard.enemyHealth = 0;

        }
        else
        {
            // deduct health from the enemy
            guard.enemyHealth -= 25;
        }

    }

    public void TakeDamage(int damage)
    {
        currentHealth = currentHealth - damage;
        hbs.value = currentHealth / maxHealth;
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
