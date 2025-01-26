using System;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class PauseHandler : MonoBehaviour
{
    public static event System.Action OnPause;
    public static event System.Action OnResume;

    private bool isPaused = false;

    private void Awake()
    {
        PlayerHealth.OnPlayerDied += StopGame;
    }

    private void Start()
    {
        ResumeGame();
    }

    private void OnDestroy()
    {
        PlayerHealth.OnPlayerDied -= StopGame;
    }

    private void OnPauseGame(InputValue inputValue)
    {
        if (isPaused)
        {
            ResumeGame();
        }
        else
        {
            PauseGame();
        }
    }
    public void RestoreTimeScale()
    {
        Time.timeScale = 1f;
    }

    public void StopGame()
    {
        isPaused = true;
        SetCursor(isPaused);
        Time.timeScale = 0f;
    }
    public void PauseGame()
    {
        StopGame();
        OnPause?.Invoke();
    }

    public void ResumeGame()
    {
        isPaused = false;
        SetCursor(isPaused);
        OnResume?.Invoke();
        Time.timeScale = 1f;
    }

    private static void SetCursor(bool isPaused)
    {
        Cursor.lockState = isPaused ? CursorLockMode.None : CursorLockMode.Locked;
        Cursor.visible = isPaused;
    }
}
