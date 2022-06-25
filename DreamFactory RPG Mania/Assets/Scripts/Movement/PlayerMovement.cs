using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class PlayerMovement : MonoBehaviour
{
    [Header("Movement Settings")]
    [SerializeField] private float _movementSpeed;

    [Header("References")]
    [SerializeField] private Animator anim;

    private Player _playerInput;
    private Rigidbody _playerRigidBody;

    private Vector3 _currentInputValue;

    private void Awake()
    {
        // InitializeMovement();
        InitializeState();
    }

    private void FixedUpdate()
    {
        ApplyInputMovement();
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
            transform.forward = _currentInputValue;
            _playerRigidBody.MovePosition(
                transform.position + (_currentInputValue * _movementSpeed * Time.deltaTime)
            );
        }
    }

    public void OnWalk(InputAction.CallbackContext ctx)
    {
        if (ctx.phase != InputActionPhase.Performed)
        {
            return;
        }

        Vector2 rawInputValue = ctx.ReadValue<Vector2>();
        if (rawInputValue == Vector2.zero)
        {
            _currentInputValue = Vector3.zero;
            anim.SetBool("IsMoving", false);

            return;
        }

        anim.SetBool("IsMoving", true);
        _currentInputValue = new Vector3(rawInputValue.x, 0f, rawInputValue.y);
    }

    public void StopMoving()
    {
        _currentInputValue = Vector3.zero;
    }
}
