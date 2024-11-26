using System;
using Jesper.InGame;
using TMPro;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.InputSystem;

public class Pausemenu : MonoBehaviour
{
    // added here for testing
    private InputAction _pauseAction;

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

    private void Pause()
    {
        if (!pause)
        {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            pause = true;
        }
        else
        {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            pause = false;
        }
    }
}
