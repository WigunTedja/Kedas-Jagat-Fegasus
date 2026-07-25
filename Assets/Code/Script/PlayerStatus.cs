using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{
    private List<string> activeStatuses = new List<string>();

    public void addStatus(string statusName)
    {
        if (!activeStatuses.Contains(statusName))
        {
            activeStatuses.Add(statusName);
            Debug.Log(statusName + "Applied to player");
        }
    }

    public void removeStatus(string statusName)
    {
        if (activeStatuses.Contains(statusName))
        {
            activeStatuses.Remove(statusName);
            Debug.Log(statusName + "Removed from Player");
        }
    }

    public bool hasStatus(string statusName)
    {
        return activeStatuses.Contains(statusName);
    }
}
