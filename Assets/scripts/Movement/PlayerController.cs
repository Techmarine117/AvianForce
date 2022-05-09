using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;
using Cinemachine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{

    public int maxHealth = 100, currentHealth;
    private Transform RespawnPosition;
    [SerializeField]
    private Slider hbs;

    public float attackRange = 4f;
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
        RespawnPosition = GameObject.FindGameObjectWithTag("Respawn").GetComponent<Transform>();
        movementAnimation.setupBehaviour();
       
        vcam = GameObject.FindGameObjectWithTag("vCam");

        currentHealth = maxHealth;
        hbs.value = currentHealth / maxHealth;
    }

    public void OnAttack(InputAction.CallbackContext Value)
    {
        if (Value.started)
        {
            Debug.Log("Attack");
            RaycastHit hit;
            movementAnimation.PlayerAttackAnimation();
            if (Physics.Raycast(transform.position, transform.forward, out hit, attackRange))
            {
                Debug.Log(hit.collider.name);
                if (hit.collider.tag == "Guard")
                {
                    guard = hit.collider.gameObject.GetComponent<Guard>();
                    Damage();
                    guard = null;
                    Debug.Log("Damaged Guard");
                }
            }
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
            guard.Healthcheck();
        }
        else
        {
            // deduct health from the enemy
            guard.enemyHealth -= 25;
            guard.Healthcheck();
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

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Ray ray = new Ray(gameObject.transform.position, transform.forward);
        Gizmos.DrawRay(ray);
        Gizmos.color = Color.green;
    }

}
