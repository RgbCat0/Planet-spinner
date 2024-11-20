using UnityEngine;

namespace Jesper.TitleScreen
{
    public class BasicCameraRotator : MonoBehaviour
    {
        [SerializeField]
        private float rotationSpeed = 10f;

        private void Update() => transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
    }
}
