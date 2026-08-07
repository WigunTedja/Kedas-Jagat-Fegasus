using System.Collections.Generic;

[System.Serializable]
public class LevelSaveData
{
    public int levelID;
    public bool isUnlocked;
    public int starsEarned;

    // A constructor to easily create new level data with default values
    public LevelSaveData(int id, bool unlocked, int stars)
    {
        levelID = id;
        isUnlocked = unlocked;
        starsEarned = stars;
    }
}