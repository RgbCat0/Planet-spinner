using UnityEngine;
using UnityEngine.InputSystem;

namespace Jesper.InGame
{
    public class RotatePlanet : MonoBehaviour // note to self: if it ain't broke, don't fix it
    {
        public bool isPlayer2;
        private InputAction _rotateAction;
        private float _rotateValue; // -1 to 1

        [SerializeField]
        private float rotationSpeed;

        private void Start()
        {
            if (!isPlayer2)
                _rotateAction = InputEntry.Instance.GameInput.RotatePlanet.Rotate;
            else
                _rotateAction = InputEntry.Instance.GameInput.RotatePlanet.RotatePlayer2;
            _rotateAction.performed += ctx => _rotateValue = ctx.ReadValue<float>() * -1;
            _rotateAction.canceled += _ => _rotateValue = 0;
        }

        private void Update() =>
            transform.Rotate(transform.forward * (_rotateValue * rotationSpeed * Time.deltaTime));
    }
}
