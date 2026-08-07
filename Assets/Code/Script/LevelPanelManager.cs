using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelPanelManager : MonoBehaviour
{
    public static LevelPanelManager Instance;
    public PlayerProfile currentProfile;

    void Awake()
    {
        Instance = this;
        LoadPlayerData();
    }

    void LoadPlayerData()
    {
        // Here you would read your JSON file and decrypt it.
        // For this example, we will simulate loading a profile.
        // If a save file exists, parse it into 'currentProfile'.
        // If not, create a new profile with Level 1 unlocked.
    }

    // A helper method for the buttons to ask for their specific data
    public LevelSaveData GetLevelData(int requestedID)
    {
        foreach (LevelSaveData level in currentProfile.levelProgress)
        {
            if (level.levelID == requestedID)
            {
                return level;
            }
        }
        return null; // Return null if the level data doesn't exist yet
    }

    // The method the buttons will call to start the game
    public void LoadLevelScene(int levelID)
    {
        Debug.Log("Loading Level: " + levelID);
        // Add your scene loading logic here
        // SceneManager.LoadScene("Level_" + levelID);
    }
}
