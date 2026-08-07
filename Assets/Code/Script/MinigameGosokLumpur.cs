using UnityEngine;

public class MinigameGosokLumpur : MinigameBase
{
    [SerializeField] private GameObject mudUI; 
    private float mudCleanedPercentage = 0f;

    public override void StartMinigame()
    {
        // Reset state
        mudCleanedPercentage = 0f;

        // Tampilkan UI minigame
        mudUI.SetActive(true);

        Debug.Log("Mulai membersihkan lumpur...");
    }

    public override void CloseMinigame()
    {
        // Sembunyikan UI
        mudUI.SetActive(false);
    }

    // Contoh fungsi yang dipanggil saat player menggesek layar/mouse
    public void ScrubAction()
    {
        mudCleanedPercentage += 10f; // Misal nambah 10% setiap gesekan

        if (mudCleanedPercentage >= 100f)
        {
            // Jika sudah bersih 100%, selesaikan minigame dengan status "Sukses" (true)
            Complete(true);
        }
    }
}
