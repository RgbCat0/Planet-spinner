using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using static UnityEngine.InputSystem.InputAction;

namespace Jesper.InGame
{
    public class PlayerMovement : MonoBehaviour
    {
        private static readonly int ToWalk = Animator.StringToHash("ToWalk");
        private static readonly int ToIdle = Animator.StringToHash("ToIdle");

        // public bool Bound { get; private set; } // unused
        private Animator _animator;
        private Rigidbody _rb;
        private Vector2 _move;
        private bool _isGroundFriction,
            _isGroundJump;
        public bool movementEnabled;
        private PlayerInput _playerInput;

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

        [SerializeField]
        private float notMoveFriction;

        [SerializeField]
        private GameObject teamCamera;

        private void Start()
        {
            _animator = GetComponent<Animator>();
            _rb = GetComponent<Rigidbody>();
            _rb.sleepThreshold = 0;
        }

        private void Update()
        {
            _isGroundFriction = IsGrounded(frictionCheckDistance);
            _isGroundJump = IsGrounded(jumpCheckDistance);
            if (movementEnabled)
                Move();
            _animator.SetTrigger(_move != Vector2.zero ? ToWalk : ToIdle);
        }

        private void Move()
        {
            transform.rotation = _move.x switch
            {
                < 0 => Quaternion.Euler(0, 90, 0),
                > 0 => Quaternion.Euler(0, -90, 0),
                _ => Quaternion.identity
            };
            _rb.AddForce(_move * (speed * Time.deltaTime), ForceMode.VelocityChange);
            if (_rb.linearVelocity.magnitude > maxSpeed)
                _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed; // cap speed
            if (_rb.linearVelocity.magnitude > 0 && _isGroundFriction)
                _rb.AddForce(
                    -_rb.linearVelocity.normalized
                        * (_move == Vector2.zero ? notMoveFriction : friction)
                ); // apply ground friction
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
            var moveAction = _playerInput.actions.FindAction("MoveLeftRight");
            moveAction.performed += OnMove;
            moveAction.canceled += OnNotMove;
            _playerInput.actions["Jump"].performed += OnJump;
            _playerInput.actions["Zoom"].performed += OnZoom;
            _playerInput.actions["Pause"].performed += OnPause;
        }

        private void OnDisable()
        { // errors and I don't know why, but I have no time to fix it
            var moveAction = _playerInput.actions.FindAction("MoveLeftRight");
            moveAction.performed -= OnMove;
            moveAction.canceled -= OnNotMove;
            _playerInput.actions["Jump"].performed -= OnJump;
            _playerInput.actions["Zoom"].performed -= OnZoom;
            _playerInput.actions["Pause"].performed -= OnPause;
        }

        private void OnMove(CallbackContext ctx) => _move = ctx.ReadValue<Vector2>();

        private void OnNotMove(CallbackContext ctx) => _move = Vector2.zero;

        private void OnJump(CallbackContext ctx) => Jump();

        private void OnZoom(CallbackContext ctx) =>
            teamCamera.GetComponent<CameraZoom>().ToggleZoom();

        private static void OnPause(CallbackContext ctx) => GameManager.Instance.PauseGame();
    }
}
