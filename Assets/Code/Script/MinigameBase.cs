using System;
using UnityEngine;

public abstract class MinigameBase : MonoBehaviour
{
    public event Action<bool> OnMinigameFinished;

    public abstract void StartMinigame();

    public abstract void CloseMinigame();

    protected void Complete(bool isSuccess)
    {
        OnMinigameFinished?.Invoke(isSuccess);
        CloseMinigame();
    }

}
