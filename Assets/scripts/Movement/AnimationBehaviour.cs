using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationBehaviour : MonoBehaviour
{
    public Animator animator;

    private int playerMovementAnimationID;
    private int playerAttackAnimationID;

    public void setupBehaviour()
    {
        setupAnimationIDs();
    }

    void setupAnimationIDs()
    {
        playerMovementAnimationID = Animator.StringToHash("Movement");
        //playerAttackAnimationID = Animator.StringToHash("Attack");
    }

    public void UpdateMovementAnimation(float movementBlendValue)
    {
        animator.SetFloat(playerMovementAnimationID, movementBlendValue);
    }

    public void PlayerAttackAnimation()
    {
        
    }

}
