using UnityEngine;
using System.IO;

public static class SaveSystem
{
    // This defines where the file lives on the device
    private static string savePath = Application.persistentDataPath + "/playerProfile.json";

    public static void SaveProfile(PlayerProfile profile)
    {
        // Convert the C# object into a JSON string (the 'true' makes the JSON readable in a text editor)
        string json = JsonUtility.ToJson(profile, true);

        // Write the text to the physical file
        Debug.Log(Application.persistentDataPath);
        File.WriteAllText(savePath, json);
    }

    public static PlayerProfile LoadProfile()
    {
        // If the file exists, read it and convert it back into a PlayerProfile object
        if (File.Exists(savePath))
        {
            Debug.Log(Application.persistentDataPath);
            string json = File.ReadAllText(savePath);
            return JsonUtility.FromJson<PlayerProfile>(json);
        }

        // If no save file exists, generate a brand new one
        PlayerProfile newProfile = new PlayerProfile();

        // Ensure Level 1 is unlocked by default for a new player
        newProfile.levelProgress.Add(new LevelSaveData(1, true, 0));

        return newProfile;
    }
}