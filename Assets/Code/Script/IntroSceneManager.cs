using UnityEngine;
using UnityEngine.SceneManagement;

public class IntroSceneManager : MonoBehaviour
{
    private const string IntroFlag = "HasSeenIntro";

    void Start()
    {
        // Best Practice: Mark it as seen the moment the scene starts.
        // If the player force-quits the game during the intro, they won't have to watch it again.
        PlayerPrefs.SetInt(IntroFlag, 1);
        PlayerPrefs.Save(); // Forces the save to disk immediately
    }

    public void SkipIntro()
    {
        TransitionToGame();
    }

    // Call this via a Timeline Signal Receiver or VideoPlayer loop point when the intro finishes naturally
    public void OnIntroFinished()
    {
        TransitionToGame();
    }

    private void TransitionToGame()
    {
        // For larger games, you should use LoadSceneAsync here to prevent freezing
        SceneManager.LoadScene("Level_Tutorial");
    }
}