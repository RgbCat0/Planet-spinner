using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Jesper.InGame
{
    public class CameraZoom : MonoBehaviour
    {
        public bool isPlayer2;
        private InputAction _zoomAction;

        [SerializeField]
        private Transform target;

        [SerializeField]
        private Vector3 zoomInDistance;

        [SerializeField]
        private Vector3 zoomOutPosition;

        [SerializeField]
        private float zoomSpeed = 1f;

        private bool _isZoomedIn;

        private void Start()
        {
            // if (!isPlayer2)
            // _zoomAction = InputEntry.Instance.GameInput.Camera.Zoom;
            // else
            // _zoomAction = InputEntry.Instance.GameInput.Camera.ZoomPlayer2;
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
                transform.localPosition = Vector3.Lerp(
                    transform.localPosition,
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
                transform.localPosition = Vector3.Lerp(
                    transform.localPosition,
                    target.localPosition + zoomInDistance,
                    zoomSpeed * Time.deltaTime
                );
                yield return null;
            }
        }
    }
}