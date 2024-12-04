using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Jesper.InGame
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

        private bool _isZoomedIn;

        public void ToggleZoom()
        {
            _isZoomedIn = !_isZoomedIn;
            StartCoroutine(_isZoomedIn ? ZoomIn() : ZoomOut());
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

        private IEnumerator ZoomIn()
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
