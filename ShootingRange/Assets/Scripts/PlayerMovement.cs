using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    public float movementSpeed = 5f;
    
    CharacterController chController;
    Vector3 rawMoveDir;

    void Start()
    {
        chController = GetComponent<CharacterController>();
    }
    
    void OnMove (InputValue value)
    {
        rawMoveDir = value.Get<Vector2>();
    }

    private void Update()
    {
        var moveDir = transform.forward * rawMoveDir.y + transform.right * rawMoveDir.x;
        chController.Move(Vector3.ClampMagnitude(moveDir, 1f) * movementSpeed * Time.deltaTime);
    }
}
