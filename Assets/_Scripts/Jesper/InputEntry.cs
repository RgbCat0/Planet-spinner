using UnityEngine;

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
    }

    private void OnEnable()
    {
        GameInput?.Enable();
        Debug.LogError("Input script enabled");
    }

    private void OnDisable()
    {
        GameInput?.Disable();
        Debug.LogError("Input script disabled");
    }
}
