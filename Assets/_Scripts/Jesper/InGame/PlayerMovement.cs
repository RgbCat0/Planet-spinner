using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

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
            _rb.AddForce(_move * (speed * Time.deltaTime), ForceMode.VelocityChange);
            if (_rb.linearVelocity.magnitude > maxSpeed)
                _rb.linearVelocity = _rb.linearVelocity.normalized * maxSpeed; // cap speed
            if (_rb.linearVelocity.magnitude > 0 && _isGroundFriction)
                _rb.AddForce(-_rb.linearVelocity.normalized * friction); // apply ground friction
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
            var moveAction = playerInput.actions.FindAction("MoveLeftRight");
            moveAction.performed += ctx => _move = ctx.ReadValue<Vector2>();
            moveAction.canceled += _ => _move = Vector2.zero;
            playerInput.actions["Jump"].performed += _ => Jump();
            playerInput.actions["Zoom"].performed += _ =>
                teamCamera.GetComponent<CameraZoom>().ToggleZoom();
            playerInput.actions["Pause"].performed += _ => GameManager.Instance.PauseGame();
        }
    }
}
