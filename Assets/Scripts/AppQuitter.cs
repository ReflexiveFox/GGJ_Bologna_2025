using UnityEngine;

public class AppQuitter : MonoBehaviour
{
    public void QuitGame()
    {
        PlayerPrefs.Save();
        Application.Quit();
    }
}
