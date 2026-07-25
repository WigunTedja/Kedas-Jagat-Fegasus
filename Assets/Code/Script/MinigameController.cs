using UnityEngine;

public class MinigameController : MonoBehaviour
{
    // This creates a global pathway to THIS specific instance
    public static MinigameController Instance { get; private set; }

    public Transform playerTransform;
    public GameObject[] minigamePanels;
    private int currentMinigameIndex = -1;

    public GameObject FinalGateObject;
    public int GateKeysReq;
    private int currentKeys = 0;

    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }

    void Start()
    {
        foreach (var panel in minigamePanels)
        {
            panel.SetActive(false);
        }
    }

    public void OpenMinigame(int minigameNumber)
    {
        if (minigameNumber < 0 || minigameNumber >= minigamePanels.Length) return;

        currentMinigameIndex = minigameNumber;
        GameObject targetObject = minigamePanels[minigameNumber];

        targetObject.SetActive(true);
        targetObject.transform.position = playerTransform.position;
        Time.timeScale = 0f;
    }

    // Dipanggil oleh objek Kunci saat berhasil didapatkan
    public void CloseActiveMinigame()
    {
        if (currentMinigameIndex >= 0 && currentMinigameIndex < minigamePanels.Length)
        {
            GameObject targetObject = minigamePanels[currentMinigameIndex];
            targetObject.SetActive(false);
            targetObject.transform.position = new Vector3(0, 0, -11);
            Time.timeScale = 1f;
            currentMinigameIndex = -1;
        }
    }

    public void GateKeyProgress()
    {
        currentKeys++;
        if(currentKeys == GateKeysReq)
        {
            Destroy(FinalGateObject);
        }
    }
}