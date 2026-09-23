using UnityEngine;
using UnityEngine.EventSystems;

public class MinigameTrashItem : MonoBehaviour, IPointerClickHandler
{
    [Header("Corresponding minigame trigger")]
    public GameObject minigameTrigger;

    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Sampah berhasil disortir!");

        // Beritahu LevelManager untuk menambah counter sampah
        if (LevelManager.Instance != null)
        {
            LevelManager.Instance.RegisterSortedTrash();
        }

        // Tutup UI minigame
        if (MinigameController.Instance != null)
        {
            MinigameController.Instance.CloseActiveMinigame();
        }

        // Hancurkan objek pemicu di world agar tidak bisa diinteraksi lagi
        if (minigameTrigger != null)
        {
            Destroy(minigameTrigger);
        }
    }
}