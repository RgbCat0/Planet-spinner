using UnityEngine;
using TMPro;
using UnityEditor.UIElements;

public class Pausemenu : MonoBehaviour
{
    [SerializeField] GameObject pauseMenu;
  [SerializeField] bool pause = false;
    bool wait = true;
    void Awake()
    {
        pauseMenu.SetActive(false);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && pause == false)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            pause = true;
        }
        if (Input.GetKeyDown(KeyCode.Escape) && pause == true && wait == false)
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            pause = false;
            wait = true;
        }
        if (pauseMenu.activeSelf)
        {
            wait = false;
        }
    }
}
