using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

[System.Serializable]
public struct LevelVisualData
{
    public int levelID;
    public string levelName;
    public Sprite levelThumbnail;
}

public class LevelPanelManager : MonoBehaviour
{
    public static LevelPanelManager Instance;
    public PlayerProfile currentProfile;

    [Header("Data Visual Level")]
    public LevelVisualData[] levelVisuals;

    [Header("Referensi UI Panel Kanan")]
    public GameObject rightSidePanel;
    public TextMeshProUGUI levelTitleText;
    public Image levelThumbnailImage;
    public GameObject[] filledStarImages; // Masukkan 3 GameObject bintang penuh ke sini
    public Button playButton;

    private int currentlySelectedLevelID;

    void Awake()
    {
        Instance = this;
        LoadPlayerData();
        rightSidePanel.SetActive(false);
    }
    void LoadPlayerData()
    {
        currentProfile = SaveSystem.LoadProfile();
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

    public void ShowLevelDetails(int levelID)
    {
        currentlySelectedLevelID = levelID;
        rightSidePanel.SetActive(true); // Tampilkan panel

        // 1. Update Teks dan Gambar Thumbnail
        foreach (LevelVisualData visual in levelVisuals)
        {
            if (visual.levelID == levelID)
            {
                levelTitleText.text = visual.levelName;
                levelThumbnailImage.sprite = visual.levelThumbnail;
                break;
            }
        }

        // 2. Update Bintang berdasarkan Save Data
        LevelSaveData savedData = GetLevelData(levelID);
        int starsEarned = (savedData != null) ? savedData.starsEarned : 0;

        for (int i = 0; i < filledStarImages.Length; i++)
        {
            // Jika index lebih kecil dari bintang yang didapat, aktifkan gambar bintang penuh
            filledStarImages[i].SetActive(i < starsEarned);
        }

        // 3. Atur Tombol Play untuk memuat level yang sedang dipilih
        playButton.onClick.RemoveAllListeners();
        playButton.onClick.AddListener(() => LoadLevelScene(currentlySelectedLevelID));
    }

    // The method the buttons will call to start the game
    public void LoadLevelScene(int levelID)
    {
        Debug.Log("Loading Level: " + levelID);
        // Add your scene loading logic here
         SceneManager.LoadScene("Level_" + levelID);
    }

    public void LoadMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
