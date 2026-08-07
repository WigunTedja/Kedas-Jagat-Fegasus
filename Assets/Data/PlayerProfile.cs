using System.Collections.Generic;

[System.Serializable]
public class PlayerProfile
{
    // This list holds the progress for every level the player interacts with
    public List<LevelSaveData> levelProgress = new List<LevelSaveData>();

    //global player variables here later
    public float masterVolume = 1.0f;
}