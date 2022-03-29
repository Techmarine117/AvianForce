using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class PlayerController : MonoBehaviour
{
    private netWorking networkingScript;
    private NetworkComponent nwc;

    public MovementBehaviour movement;
    public AnimationBehaviour movementAnimation;
    public PlayerInput playerInput;
    public float moventSmoothingSpeed = 1f;
    private Vector3 rawInputMovement;
    private Vector3 smoothInput;

    private string currentControlScheme;

    private void Start()
    {
        movementAnimation.setupBehaviour();
        networkingScript = FindObjectOfType<netWorking>();
        nwc = GetComponent<NetworkComponent>();
    }

    

    public void OnMove(InputAction.CallbackContext value)
    {
        Vector2 inputMovement = value.ReadValue<Vector2>();
        rawInputMovement = new Vector3(inputMovement.x, 0, inputMovement.y);
        
    }

    private void Update()
    {
        if (true) //networkingScript.GetPlayerID() == nwc.OwnerID)
        {
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

    
}
