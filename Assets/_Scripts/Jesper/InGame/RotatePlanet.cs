using UnityEngine;
using UnityEngine.InputSystem;

namespace Jesper.InGame
{
    public class RotatePlanet : MonoBehaviour // note to self: if it ain't broke, don't fix it
    {
        public bool Bound { get; private set; }
        public bool movementEnabled;
        private float _rotateValue; // -1 to 1

        [SerializeField]
        private float rotationSpeed;

        [SerializeField]
        private GameObject teamCamera;

        private void Update()
        {
            if (movementEnabled)
                transform.Rotate(
                    transform.forward * (_rotateValue * rotationSpeed * Time.deltaTime)
                );
        }

        public void BindPlayerInput(PlayerInput playerInput)
        {
            if (Bound)
                return;
            var rotateAction = playerInput.actions["Rotate"];
            rotateAction.performed += ctx => _rotateValue = ctx.ReadValue<float>() * -1;
            rotateAction.canceled += _ => _rotateValue = 0;
            var temp = playerInput.actions["Zoom"];
            temp.performed += _ => teamCamera.GetComponent<CameraZoom>().ToggleZoom();
            Bound = true;
        }
    }
}
