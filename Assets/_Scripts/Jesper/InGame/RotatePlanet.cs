using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

namespace Jesper.InGame
{
    public class RotatePlanet : MonoBehaviour // note to self: if it ain't broke, don't fix it
    {
        public bool Bound { get; private set; }
        private InputAction _rotateAction;
        private float _rotateValue; // -1 to 1

        [SerializeField]
        private float rotationSpeed;

        private void Update() =>
            transform.Rotate(transform.forward * (_rotateValue * rotationSpeed * Time.deltaTime));

        public void BindPlayerInput(PlayerInput playerInput)
        {
            if (Bound)
                return;
            _rotateAction = playerInput.actions["Rotate"];
            _rotateAction.performed += ctx => _rotateValue = ctx.ReadValue<float>() * -1;
            _rotateAction.canceled += _ => _rotateValue = 0;
            Bound = true;
        }
    }
}
