using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    public TMPro.TextMeshProUGUI timerText;

    [Header("Level Identity")]
    public int currentLevelID;

    [Header("Timer & Stars (in seconds)")]
    public float timeForThreeStars = 60f;
    public float timeForTwoStars = 45f;

    [Header("Level Requirements")]
    public int totalTrashInLevel;
    private int currentTrashSorted = 0;

    public float levelTimer = 300f;
    private float currentTime;
    private bool isTimerRunning = false;

    [Header("UI Victory")]
    public GameObject victoryUI;
    public GameObject gameOverUI;
    public TMPro.TextMeshProUGUI timeSpentText;

    //[Header("UI")]
    //public GameObject pauseUI;

    [Header("UI Pause")]
    public GameObject pauseUI;

    void Start()
    {
        currentTime = levelTimer;
        totalTrashInLevel = GameObject.FindGameObjectsWithTag("Trash").Length;
        isTimerRunning = true;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            currentTime -= Time.deltaTime;
        }
        timerText.text = currentTime.ToString("F1") + "d";
        if (currentTime < 0f)
        {
            GameOver();
        }
    }

    public void RegisterSortedTrash()
    {
        currentTrashSorted++;

        if (currentTrashSorted >= totalTrashInLevel)
        {
            LevelComplete();
        }
    }
    private void LevelComplete()
    {
        isTimerRunning = false;
        Time.timeScale = 0f;

        Debug.Log("Level Complete! All trash sorted.");
        Debug.Log("Time taken: " + levelTimer.ToString("F2") + " seconds.");
        float timeSpent = levelTimer - currentTime;
        timeSpentText.text = timeSpent.ToString("F1") + "d";

        int earnedStars = CalculateStars();
        Debug.Log("Stars Earned: " + earnedStars);

        SaveLevelProgress(earnedStars);

        if (victoryUI != null)
        {
            victoryUI.SetActive(true);
            for(int i = 0; i < earnedStars; i++)
            {
                victoryUI.transform.GetChild(i).gameObject.SetActive(true);
            }
        }
        //Time.timeScale = 0f;
        // TO DO : musik, tampilan menang dan skor bintang
    }
    private void GameOver()
    {
        gameOverUI.SetActive(true);
        Time.timeScale = 0f;
    }

    private int CalculateStars()
    {
        float timeSpent = levelTimer - currentTime;
        if (timeSpent <= timeForThreeStars)
        {
            return 3;
        }
        else if (timeSpent <= timeForTwoStars)
        {
            return 2;
        }
        else
        {
            return 1;
        }
    }

    private void SaveLevelProgress(int newStars)
    {
        // 1. Load the current profile from disk
        PlayerProfile profile = SaveSystem.LoadProfile();

        // 2. Find the data for the current level
        LevelSaveData currentLevelData = profile.levelProgress.Find(level => level.levelID == currentLevelID);

        if (currentLevelData != null)
        {
            // Only overwrite the stars if the player beat their previous high score
            if (newStars > currentLevelData.starsEarned)
            {
                currentLevelData.starsEarned = newStars;
            }
        }
        else
        {
            // Failsafe: if the level data didn't exist, create it
            profile.levelProgress.Add(new LevelSaveData(currentLevelID, true, newStars));
        }

        // 3. Unlock the next level automatically
        int nextLevelID = currentLevelID + 1;
        LevelSaveData nextLevelData = profile.levelProgress.Find(level => level.levelID == nextLevelID);

        if (nextLevelData != null)
        {
            nextLevelData.isUnlocked = true;
        }
        else
        {
            // If the next level isn't in the list yet, add it as unlocked with 0 stars
            profile.levelProgress.Add(new LevelSaveData(nextLevelID, true, 0));
        }

        // 4. Save the updated profile back to the JSON file
        SaveSystem.SaveProfile(profile);
        Debug.Log("Progress saved successfully to: " + Application.persistentDataPath);
    }

    public void PauseGame()
    {
        isTimerRunning = false; 
        pauseUI.SetActive(true);
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        pauseUI.SetActive(false);
        Time.timeScale = 1f;
        isTimerRunning = true; 
    }

    public void GoToLevelPanel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_Panel");
    }

    public void RetryLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void FinishLevel()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("Level_panel");
    }
}
