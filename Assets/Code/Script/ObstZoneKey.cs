using UnityEngine;

public class ObstZoneKey : MonoBehaviour, IInteractable
{
    public int minigameNumber;
    public void Interact(GameObject interactor)
    {
        MinigameController.Instance.OpenMinigame(minigameNumber);
    }
}
