using Unity.VisualScripting;
using UnityEngine;

public class MinigameInteractable : MonoBehaviour, IInteractable
{
    [SerializeField] private MinigameBase minigame;
    [SerializeField] private bool givesKeyReward = false;
    public void Interact(GameObject interactor)
    {
        if (minigame != null)
        {
            // Daftarkan fungsi untuk mendengarkan hasil minigame
            minigame.OnMinigameFinished += HandleMinigameResult;

            // Kunci pergerakan player di sini jika diperlukan

            minigame.StartMinigame();
        }
    }
    private void HandleMinigameResult(bool success)
    {
        // Langsung cabut pendaftaran event agar tidak memory leak
        minigame.OnMinigameFinished -= HandleMinigameResult;

        if (success)
        {
            Debug.Log("Minigame Berhasil diselesaikan!");
            if (givesKeyReward)
            {
                // Panggil sistem inventory Anda di sini
                // InventoryManager.Instance.AddItem("ZoneKey");
                Debug.Log("Player mendapatkan Kunci Gerbang Zona Terakhir!");
            }
        }
        else
        {
            Debug.Log("Player gagal atau keluar dari minigame.");
        }

        // Kembalikan pergerakan player di sini
    }
}
