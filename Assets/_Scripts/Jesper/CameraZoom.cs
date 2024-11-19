using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Jesper
{
    public class CameraZoom : MonoBehaviour
    {
        private InputAction _zoomAction;

        [SerializeField]
        private Transform target;

        [SerializeField]
        private Vector3 zoomInDistance;

        [SerializeField]
        private Vector3 zoomOutPosition;

        [SerializeField]
        private float zoomSpeed = 1f;

        [SerializeField]
        private bool _isZoomedIn;

        private void Start()
        {
            _zoomAction = InputEntry.Instance.GameInput.Camera.Zoom;
            _zoomAction.Enable();
            _zoomAction.performed += _ => ToggleZoom();
        }

        private void ToggleZoom()
        {
            Debug.Log("Toggle zoom");
            if (_isZoomedIn)
            {
                _isZoomedIn = false;
                StartCoroutine(ZoomOut());
            }
            else
            {
                _isZoomedIn = true;
                StartCoroutine(KeepCameraOnTarget());
            }
        }

        private IEnumerator ZoomOut()
        {
            while (Vector3.Distance(transform.position, zoomOutPosition) > 0.1f && !_isZoomedIn)
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    zoomOutPosition,
                    zoomSpeed * Time.deltaTime
                );
                yield return null;
            }
        }

        private IEnumerator KeepCameraOnTarget()
        {
            while (_isZoomedIn)
            {
                transform.position = Vector3.Lerp(
                    transform.position,
                    target.position + zoomInDistance,
                    zoomSpeed * Time.deltaTime
                );
                yield return null;
            }
        }
    }
}
