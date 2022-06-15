using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _movementSpeed;

    private Player _playerInput;
    private Rigidbody _playerRigidBody;

    private Vector3 _currentInputValue;

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
        _playerInput = new Player();
        _playerInput.Movement.Walk.performed += OnWalkPerformed;

        _playerInput.Enable();
    }

    private void InitializeState()
    {
        _currentInputValue = Vector2.zero;
        _playerRigidBody = GetComponent<Rigidbody>();
    }

    private void ApplyInputMovement()
    {
        if (_currentInputValue != Vector3.zero)
        {
            _playerRigidBody.MovePosition(
                transform.position + (_currentInputValue * _movementSpeed * Time.deltaTime)
            );
        }
    }

    private void OnWalkPerformed(InputAction.CallbackContext ctx)
    {
        Vector2 rawInputValue = ctx.ReadValue<Vector2>();
        if (rawInputValue == Vector2.zero)
        {
            _currentInputValue = Vector3.zero;
            return;
        }

        _currentInputValue = new Vector3(rawInputValue.x, 0f, rawInputValue.y);
    }
}
