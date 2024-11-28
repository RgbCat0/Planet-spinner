using UnityEngine;
using UnityEngine.InputSystem;

namespace Jesper.TitleScreen
{
    public class TitlePlayerMovement : MonoBehaviour
    {
        private Vector2 _moveValue;
        private RectTransform _rectTransform;

        [SerializeField]
        private float movementSpeed = 10f;

        [SerializeField]
        private Vector2 movementBounds;

        private void Start() => _rectTransform = GetComponent<RectTransform>();

        public void Bind(PlayerInput playerInput)
        {
            var action = playerInput.actions.FindAction("UiNav");
            action.performed += ctx => _moveValue = ctx.ReadValue<Vector2>();
            action.canceled += _ => _moveValue = Vector2.zero;
        }

        private void Update()
        {
            _rectTransform.anchoredPosition += _moveValue * (Time.deltaTime * movementSpeed);
            _rectTransform.anchoredPosition += _moveValue * (Time.deltaTime * movementSpeed);
            _rectTransform.anchoredPosition = new Vector2(
                Mathf.Clamp(_rectTransform.anchoredPosition.x, -movementBounds.x, movementBounds.x),
                Mathf.Clamp(_rectTransform.anchoredPosition.y, -movementBounds.y, movementBounds.y)
            );
        }
    }
}
