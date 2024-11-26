using UnityEngine;

namespace Jesper.InGame
{
    public class InputEntry : MonoBehaviour
    {
        public static InputEntry Instance { get; private set; }
        public GameInput GameInput { get; private set; }

        private void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }
            Instance = this;
            GameInput = new GameInput();
            GameInput.Enable();
            DontDestroyOnLoad(this); // keyboard testing in a controller game
        }

        private void OnEnable()
        {
            GameInput?.Enable();
            Debug.Log("Testing input script enabled");
        }

        private void OnDisable()
        {
            GameInput?.Disable();
            Debug.Log("Testing input script disabled");
        }
    }
}
