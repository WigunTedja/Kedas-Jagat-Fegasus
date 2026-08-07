using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private const string IntroFlag = "HasSeenIntro";

    public void OnPlayButtonPressed()
    {
        // Check if the player has seen the intro. We use 0 as the default if the key doesn't exist.
        if (PlayerPrefs.GetInt(IntroFlag, 0) == 0)
        {
            SceneManager.LoadScene("Game_Intro");
        }
        else
        {
            SceneManager.LoadScene("Level_Panel");
        }
    }

    public void QuitGame()
    {
        Debug.Log("Exiting Game");
        Application.Quit();
    }
}
