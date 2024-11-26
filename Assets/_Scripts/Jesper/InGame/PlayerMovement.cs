using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Jesper.InGame
{
    public class PlayerMovement : MonoBehaviour
    {
        public bool Bound { get; private set; }
        private Rigidbody _rb;
        private PlayerInput _playerInput;
        private InputAction _moveAction;
        private InputAction _jumpAction;
        private Vector2 _moveValue;
        private bool _isGroundFriction,
            _isGroundJump;

        [SerializeField]
        private float speed;

        [SerializeField]
        private float maxSpeed;

        [SerializeField]
        private float jumpForce;

        [
            FormerlySerializedAs("groundCheckDistance"),
            SerializeField,
            Tooltip("Distance to check if the player is grounded")
        ]
        private float jumpCheckDistance;

        [SerializeField]
        private float frictionCheckDistance; // wanted a bit more check for friction then jump

        [SerializeField, Tooltip("How much the player slows down when grounded")]
        private float friction;

        private void Start()
        {
            _rb = GetComponent<Rigidbody>();
            _rb.sleepThreshold = 0;
        }

        private void Update()
        {
            _isGroundFriction = IsGrounded(frictionCheckDistance);
            _isGroundJump = IsGrounded(jumpCheckDistance);
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
            if (_rb.linearVelocity.magnitude > 0 && _isGroundFriction)
                _rb.AddForce(-_rb.linearVelocity.normalized * friction);
        }

        private void Jump()
        {
            if (_isGroundJump)
                _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }

        private bool IsGrounded(float distance) =>
            Physics.Raycast(transform.position, Vector3.down, distance);

        public void BindPlayerInput(PlayerInput playerInput)
        {
            _playerInput = playerInput;
            _moveAction = _playerInput.actions.FindAction("MoveLeftRight");
            _jumpAction = _playerInput.actions.FindAction("Jump");
            _moveAction.Enable();
            _jumpAction.Enable();
            _moveAction.performed += ctx => _moveValue = ctx.ReadValue<Vector2>();
            _moveAction.canceled += _ => _moveValue = Vector2.zero;
            _jumpAction.performed += _ => Jump();
            Bound = true;
        }
    }
}
