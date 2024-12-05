using System;
using Jesper.InGame;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class PauseMenu : MonoBehaviour
{
    /* unused script
    // added here for testing
    [SerializeField]private InputAction _pauseAction;

    private void Awake() { }

    [SerializeField]
    GameObject pauseMenu;

    [SerializeField]
    bool pause = false;
    bool wait = true;

    void Start()
    {
        _pauseAction = InputEntry.Instance.GameInput.Player.Pause;
        _pauseAction.performed += _ => Pause();
        pauseMenu.SetActive(false);
    }

    private void Pause()
    {
        if (!pause)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            pause = true;
            Debug.Log("yes it work");
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            pause = false;
        }
    }
    */
}
