using UnityEngine;
using UnityEngine.UI; // Required to interact with Canvas Images

public class VisionUIManager : MonoBehaviour
{
    [Header("References")]
    [Tooltip("Drag your Player GameObject here")]
    public PlayerStatus playerStatus;

    [Tooltip("Drag the Smoke Overlay Image from your Canvas here")]
    public Image smokeOverlay;

    [Header("Settings")]
    [Tooltip("How fast the smoke fades in and out")]
    public float fadeSpeed = 5f;

    void Update()
    {
        // 1. Is the player currently blinded?
        bool isBlinded = playerStatus.hasStatus("Blinded");

        // 2. Set the target transparency (Alpha). 1 is fully visible, 0 is invisible.
        float targetAlpha = isBlinded ? 1f : 0f;

        // 3. Get the current color of the UI Image
        Color currentColor = smokeOverlay.color;

        // 4. Mathf.Lerp smoothly transitions the current alpha towards the target alpha
        currentColor.a = Mathf.Lerp(currentColor.a, targetAlpha, Time.deltaTime * fadeSpeed);

        // 5. Apply the new color back to the UI Image
        smokeOverlay.color = currentColor;
    }
}