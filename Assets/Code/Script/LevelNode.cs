using UnityEngine;
using UnityEngine.UI;

public class LevelNode : MonoBehaviour
{
    public int levelID;

    [Header("UI References")]
    public Button nodeButton;
    public GameObject lockedGraphic;
    public GameObject unlockedGraphic;
    public GameObject[] stars; // Assign your 3 star GameObjects in the Inspector

    void Start()
    {
        RefreshNode();
    }

    public void RefreshNode()
    {
        // Ask the central manager for this specific level's data
        LevelSaveData data = LevelPanelManager.Instance.GetLevelData(levelID);

        // If the level is locked or has no data
        if (data == null || data.isUnlocked == false)
        {
            nodeButton.interactable = false;
            lockedGraphic.SetActive(true);
            unlockedGraphic.SetActive(false);
        }
        // If the level is unlocked
        else
        {
            nodeButton.interactable = true;
            lockedGraphic.SetActive(false);
            unlockedGraphic.SetActive(true);

            // Turn on the correct number of stars
            for (int i = 0; i < stars.Length; i++)
            {
                // If i is less than starsEarned, turn the star on. Otherwise, turn it off.
                stars[i].SetActive(i < data.starsEarned);
            }
        }

        // Add the click event via code so you don't have to do it in the Inspector
        nodeButton.onClick.RemoveAllListeners();
        nodeButton.onClick.AddListener(() => LevelPanelManager.Instance.ShowLevelDetails(levelID));
    }
}
