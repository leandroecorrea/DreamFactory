using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float movementSpeed;

    private Player playerInput;
    private Rigidbody playerRigidBody;

    private Vector3 currentInputValue;

    private void Awake()
    {
        InitializeMovement();
        InitializeState();
    }

    private void FixedUpdate()
    {
        ApplyInputMovement();
    }

    private void InitializeMovement()
    {
        playerInput = new Player();
        playerInput.Movement.Walk.performed += OnWalkPerformed;

        playerInput.Enable();
    }

    private void InitializeState()
    {
        currentInputValue = Vector2.zero;
        playerRigidBody = GetComponent<Rigidbody>();
    }

    private void ApplyInputMovement()
    {
        if (currentInputValue != Vector3.zero)
        {
            playerRigidBody.MovePosition(
                transform.position + (currentInputValue * movementSpeed * Time.deltaTime)
            );
        }
    }

    private void OnWalkPerformed(InputAction.CallbackContext ctx)
    {
        Vector2 rawInputValue = ctx.ReadValue<Vector2>();
        if (rawInputValue == Vector2.zero)
        {
            currentInputValue = Vector3.zero;
            return;
        }

        currentInputValue = new Vector3(rawInputValue.x, 0f, rawInputValue.y);
    }
}
