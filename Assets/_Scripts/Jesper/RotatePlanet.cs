using UnityEngine;
using UnityEngine.InputSystem;

public class RotatePlanet : MonoBehaviour
{
    private InputAction _rotateAction;
    private float _rotateValue; // -1 to 1

    [SerializeField]
    private float rotationSpeed;

    private void Start()
    {
        _rotateAction = InputEntry.Instance.GameInput.RotatePlanetPlayer.RotatePlanet;
        _rotateAction.performed += ctx => _rotateValue = ctx.ReadValue<float>();
        _rotateAction.canceled += _ => _rotateValue = 0;
    }

    private void Update()
    {
        var rotation = transform.forward * (_rotateValue * rotationSpeed * -1 * Time.deltaTime);
        transform.Rotate(rotation);
    }
}
