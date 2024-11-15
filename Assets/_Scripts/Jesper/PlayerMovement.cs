using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private InputAction _moveAction;
    private Vector2 _moveValue;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.sleepThreshold = 0;
        _moveAction = InputEntry.Instance.GameInput.PlayerMovement.MoveLeftRight;
        _moveAction.performed += ctx => _moveValue = ctx.ReadValue<Vector2>();
        _moveAction.canceled += _ => _moveValue = Vector2.zero;
    }

    private void Update()
    {
        transform.Translate(_moveValue.x * Time.deltaTime, 0, 0);
    }
}
