using System.Collections;
using UnityEngine;
using TMPro;

public class LevelStartManager : MonoBehaviour
{
    [Header("UI References")]
    public TextMeshProUGUI countdownText;
    public GameObject countdownPanel;

    [Header("Settings")]
    public int startCountdownTime = 3;

    void Start()
    {
        StartCoroutine(LevelStartRoutine());
    }

    IEnumerator LevelStartRoutine()
    {
        Time.timeScale = 0f;

        if (countdownPanel != null) countdownPanel.SetActive(true);

        int timer = startCountdownTime;

        while (timer > 0)
        {
            countdownText.text = timer.ToString();

            yield return new WaitForSecondsRealtime(1f);
            timer--;
        }

        countdownText.text = "MULAI!";
        yield return new WaitForSecondsRealtime(0.5f); 

        if (countdownPanel != null) countdownPanel.SetActive(false);

        Time.timeScale = 1f;

    }
}