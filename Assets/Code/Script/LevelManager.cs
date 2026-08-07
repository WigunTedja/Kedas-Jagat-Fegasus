using UnityEngine;

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

    private float levelTimer = 300f;
    private bool isTimerRunning = false;

    [Header("UI")]
    public GameObject victoryUI;

    void Start()
    {
        totalTrashInLevel = GameObject.FindGameObjectsWithTag("Trash").Length;
        isTimerRunning = true;
    }

    void Update()
    {
        if (isTimerRunning)
        {
            levelTimer -= Time.deltaTime;
        }
        timerText.text = levelTimer.ToString("F1") + "s";
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
        Debug.Log("Level Complete! All trash sorted.");
        Debug.Log("Time taken: " + levelTimer.ToString("F2") + " seconds.");

        int earnedStars = CalculateStars();
        Debug.Log("Stars Earned: " + earnedStars);

        SaveLevelProgress(earnedStars);

        if (victoryUI != null)
        {
            victoryUI.SetActive(true);
        }
        //Time.timeScale = 0f;
        // TO DO : musik, tampilan menang dan skor bintang
    }

    private int CalculateStars()
    {
        if (levelTimer <= timeForThreeStars)
        {
            return 3;
        }
        else if (levelTimer <= timeForTwoStars)
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
}
