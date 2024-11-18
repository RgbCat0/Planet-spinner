using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    private Rigidbody _rb;
    private InputAction _moveAction;
    private InputAction _jumpAction;
    private Vector2 _moveValue;
    private bool _isGrounded;

    [SerializeField]
    private float speed;

    [SerializeField]
    private float maxSpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField, Tooltip("Distance to check if the player is grounded")]
    private float groundCheckDistance;

    [SerializeField, Tooltip("How much the player slows down when grounded")]
    private float friction;

    private void Start()
    {
        _rb = GetComponent<Rigidbody>();
        _rb.sleepThreshold = 0;
        _moveAction = InputEntry.Instance.GameInput.PlayerMovement.MoveLeftRight;
        _jumpAction = InputEntry.Instance.GameInput.PlayerMovement.Jump;
        _moveAction.Enable();
        _jumpAction.Enable();
        _moveAction.performed += ctx => _moveValue = ctx.ReadValue<Vector2>();
        _moveAction.canceled += _ => _moveValue = Vector2.zero;
        _jumpAction.performed += _ => Jump();
    }

    private void Update()
    {
        _isGrounded = IsGrounded();
        Move();
    }

    private void Move()
    {
        _rb.AddForce(
            new Vector3(_moveValue.x, 0, _moveValue.y) * (speed * Time.deltaTime),
            ForceMode.VelocityChange
        );
        if (_rb.linearVelocity.magnitude > maxSpeed)
            _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed;
        if (_rb.linearVelocity.magnitude > 0 && _isGrounded)
            _rb.AddForce(-_rb.linearVelocity.normalized * friction);
    }

    private void Jump()
    {
        if (_isGrounded)
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    private bool IsGrounded() =>
        Physics.Raycast(transform.position, Vector3.down, groundCheckDistance);
}
