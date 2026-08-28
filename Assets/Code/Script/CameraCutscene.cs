using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class CameraCutscene : MonoBehaviour
{
    [System.Serializable]
    public class CameraPoint
    {
        [Tooltip("Empty GameObject yang menjadi tujuan kamera.")]
        public Transform target;

        [Min(0f)]
        [Tooltip("Berapa detik kamera bergerak menuju titik ini.")]
        public float moveDuration = 2f;

        [Min(0f)]
        [Tooltip("Berapa detik kamera diam setelah sampai di titik ini.")]
        public float holdDuration = 1f;
    }

    [Header("Camera")]
    [SerializeField]
    private Camera targetCamera;

    [SerializeField]
    private CameraFollow cameraFollow;

    [Header("Start Settings")]
    [Tooltip("Jalankan cutscene otomatis ketika scene dimulai.")]
    [SerializeField]
    private bool playOnStart = false;

    [Min(0f)]
    [Tooltip("Delay sebelum cutscene otomatis dimulai.")]
    [SerializeField]
    private float startDelay = 0f;

    [Header("Camera Points")]
    [SerializeField]
    private CameraPoint[] cameraPoints;

    [Header("Movement")]
    [Tooltip("Gunakan gerakan halus saat berpindah antar titik.")]
    [SerializeField]
    private bool smoothMovement = true;

    [Header("End Settings")]
    [Tooltip("Kembalikan kamera ke player setelah semua Camera Point selesai.")]
    [SerializeField]
    private bool returnToPlayer = true;

    [Min(0f)]
    [Tooltip("Waktu yang diperlukan kamera untuk kembali ke player.")]
    [SerializeField]
    private float returnDuration = 2f;

    [Header("Events")]
    public UnityEvent onCutsceneFinished;

    public bool IsPlaying { get; private set; }

    private bool cameraFollowWasEnabled;

    private void Start()
    {
        // Kalau kamera belum diisi,
        // coba cari Main Camera secara otomatis.
        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        if (playOnStart)
        {
            StartCoroutine(PlayOnStartRoutine());
        }
    }

    private IEnumerator PlayOnStartRoutine()
    {
        if (startDelay > 0f)
        {
            yield return new WaitForSecondsRealtime(startDelay);
        }

        PlayCutscene();
    }

    /// <summary>
    /// Bisa dipanggil dari script lain,
    /// UnityEvent, Button, Signal Receiver, dll.
    /// </summary>
    public void PlayCutscene()
    {
        if (IsPlaying)
        {
            Debug.LogWarning(
                $"Camera cutscene pada {gameObject.name} sedang berjalan."
            );
            return;
        }

        if (targetCamera == null)
        {
            targetCamera = Camera.main;
        }

        if (targetCamera == null)
        {
            Debug.LogError(
                "CameraCutscene: Tidak menemukan kamera."
            );
            return;
        }

        StartCoroutine(CutsceneRoutine());
    }

    private IEnumerator CutsceneRoutine()
    {
        IsPlaying = true;

        // =========================
        // Matikan CameraFollow
        // =========================

        if (cameraFollow != null)
        {
            cameraFollowWasEnabled = cameraFollow.enabled;
            cameraFollow.enabled = false;
        }

        // =========================
        // Jalankan semua Camera Point
        // =========================

        if (cameraPoints != null)
        {
            foreach (CameraPoint point in cameraPoints)
            {
                if (point == null || point.target == null)
                {
                    Debug.LogWarning(
                        "CameraCutscene: Ada Camera Point yang kosong."
                    );
                    continue;
                }

                yield return MoveCamera(
                    point.target.position,
                    point.moveDuration
                );

                if (point.holdDuration > 0f)
                {
                    yield return new WaitForSecondsRealtime(
                        point.holdDuration
                    );
                }
            }
        }

        // =========================
        // Kembali ke Player
        // =========================

        if (returnToPlayer)
        {
            yield return ReturnCameraToPlayer();

            // Aktifkan CameraFollow kembali,
            // hanya jika sebelumnya memang aktif.
            if (cameraFollow != null)
            {
                cameraFollow.enabled =
                    cameraFollowWasEnabled;
            }
        }

        // Jika Return To Player mati,
        // CameraFollow sengaja tetap mati
        // agar kamera tetap di titik terakhir.

        IsPlaying = false;

        // Jalankan event setelah cutscene selesai.
        onCutsceneFinished?.Invoke();
    }

    private IEnumerator ReturnCameraToPlayer()
    {
        if (cameraFollow == null)
        {
            Debug.LogWarning(
                "CameraCutscene: CameraFollow belum dimasukkan. " +
                "Tidak bisa menentukan posisi player."
            );

            yield break;
        }

        if (cameraFollow.player == null)
        {
            Debug.LogWarning(
                "CameraCutscene: Player pada CameraFollow kosong."
            );

            yield break;
        }

        Vector3 playerPosition =
            cameraFollow.player.position +
            cameraFollow.offset;

        yield return MoveCamera(
            playerPosition,
            returnDuration
        );
    }

    private IEnumerator MoveCamera(
        Vector3 targetPosition,
        float duration
    )
    {
        Transform cameraTransform =
            targetCamera.transform;

        Vector3 startPosition =
            cameraTransform.position;

        // Untuk game 2D:
        // Camera Point hanya menentukan X dan Y.
        // Z kamera tetap dipertahankan.
        Vector3 finalPosition = new Vector3(
            targetPosition.x,
            targetPosition.y,
            startPosition.z
        );

        // Duration 0 = langsung pindah.
        if (duration <= 0f)
        {
            cameraTransform.position =
                finalPosition;

            yield break;
        }

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime +=
                Time.unscaledDeltaTime;

            float t =
                Mathf.Clamp01(
                    elapsedTime / duration
                );

            if (smoothMovement)
            {
                // Smooth start dan smooth stop.
                t = Mathf.SmoothStep(
                    0f,
                    1f,
                    t
                );
            }

            cameraTransform.position =
                Vector3.Lerp(
                    startPosition,
                    finalPosition,
                    t
                );

            yield return null;
        }

        cameraTransform.position =
            finalPosition;
    }

    private void OnDisable()
    {
        // Safety jika object dimatikan
        // ketika cutscene masih berlangsung.
        if (IsPlaying && cameraFollow != null)
        {
            cameraFollow.enabled =
                cameraFollowWasEnabled;
        }

        IsPlaying = false;
    }
}