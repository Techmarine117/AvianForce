using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementBehaviour : MonoBehaviour
{
    public Rigidbody prb;
    public Camera cam;

    public float moveSpeed = 3f;
    public float turnSpeed = 0.1f;

    private Vector3 moveDirection;

    

    public void UpdateMoveentData(Vector3 newMoveDirection)
    {
        moveDirection = newMoveDirection;
    }

    private void FixedUpdate()
    {
        MovePlayer();
        TurnPlayer();
    }

    void MovePlayer()
    {
        Vector3 movement = CamDirection(moveDirection) * moveSpeed * Time.deltaTime;
        prb.MovePosition(transform.position + movement);
        Debug.Log("movetest");
    }

    void TurnPlayer()
    {
        if(moveDirection.sqrMagnitude > 0.01f)
        {
            Quaternion rotation = Quaternion.Slerp(prb.rotation, Quaternion.LookRotation(CamDirection(moveDirection)),turnSpeed);

            prb.MoveRotation(rotation);
        }
    }

    Vector3 CamDirection(Vector3 moveDirection)
    {
       Vector3 camForward = cam.transform.forward;
       Vector3 camRight = cam.transform.right;

        camForward.y = 0f;
        camRight.y = 0f;
        return camForward * moveDirection.z + camRight * moveDirection.x;
    }
}
