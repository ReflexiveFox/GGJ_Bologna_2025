using UnityEngine;
using UnityEngine.InputSystem;

public class PauseHandler : MonoBehaviour
{
    public static event System.Action OnPause;
    public static event System.Action OnResume;

    private bool isPaused = false;

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

    public void PauseGame()
    {
        isPaused = true;
        OnPause?.Invoke();
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        OnResume?.Invoke();
        Time.timeScale = 1f;
    }
}
