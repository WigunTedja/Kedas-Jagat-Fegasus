using System.Collections;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class TimelineDialogueTrigger : MonoBehaviour
{
    // =========================================================
    // DIALOGUE
    // =========================================================

    [Header("Dialogue")]
    [SerializeField]
    private DialogueSO dialogue;


    // =========================================================
    // AUTO START
    // =========================================================

    [Header("Auto Start")]

    [Tooltip("Jika aktif, dialogue otomatis muncul ketika scene dimulai.")]
    [SerializeField]
    private bool playOnSceneStart = false;

    [Tooltip("Delay sebelum dialogue otomatis dimulai.")]
    [SerializeField]
    private float startDelay = 0f;


    // =========================================================
    // TIMELINE
    // =========================================================

    [Header("Timeline")]

    [Tooltip("Boleh dikosongkan jika dialogue tidak menggunakan Timeline.")]
    [SerializeField]
    private PlayableDirector director;

    [Tooltip("Pause Timeline selama dialogue berlangsung.")]
    [SerializeField]
    private bool pauseTimelineDuringDialogue = true;


    // =========================================================
    // SCENE TRANSITION
    // =========================================================

    [Header("Scene Transition")]

    [Tooltip("Jika aktif, dialogue selesai akan pindah ke scene tujuan.")]
    [SerializeField]
    private bool goToSceneOnFinish = false;


#if UNITY_EDITOR

    [Tooltip("Pilih Scene tujuan.")]
    [SerializeField]
    private SceneAsset targetScene;

#endif


    [HideInInspector]
    [SerializeField]
    private string targetSceneName;


    // =========================================================
    // INTERNAL
    // =========================================================

    private bool waitingForDialogue = false;


    // =========================================================
    // START
    // =========================================================

    private void Start()
    {
        if (playOnSceneStart)
        {
            StartCoroutine(StartDialogueWithDelay());
        }
    }


    private IEnumerator StartDialogueWithDelay()
    {
        if (startDelay > 0f)
        {
            yield return new WaitForSecondsRealtime(startDelay);
        }

        PlayDialogue();
    }


    // =========================================================
    // PLAY DIALOGUE
    // =========================================================

    public void PlayDialogue()
    {
        if (waitingForDialogue)
            return;


        if (dialogue == null)
        {
            Debug.LogWarning(
                $"Dialogue belum dipasang pada {gameObject.name}."
            );

            return;
        }


        if (DialogueManager.Instance == null)
        {
            Debug.LogError(
                "DialogueManager tidak ditemukan di Scene."
            );

            return;
        }


        if (DialogueManager.Instance.IsDialogueActive)
        {
            Debug.LogWarning(
                "Dialogue lain sedang berjalan."
            );

            return;
        }


        waitingForDialogue = true;


        // Pause Timeline
        if (pauseTimelineDuringDialogue && director != null)
        {
            director.Pause();
        }


        // Dengarkan event dialogue selesai
        DialogueManager.Instance.OnDialogueFinished +=
            HandleDialogueFinished;


        // Mulai dialogue
        DialogueManager.Instance.StartDialogue(dialogue);
    }


    // =========================================================
    // DIALOGUE FINISHED
    // =========================================================

    private void HandleDialogueFinished()
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.OnDialogueFinished -=
                HandleDialogueFinished;
        }


        waitingForDialogue = false;


        Debug.Log("Dialogue selesai.");


        // Jika harus pindah scene
        if (goToSceneOnFinish)
        {
            LoadTargetScene();
            return;
        }


        // Kalau tidak pindah scene,
        // lanjutkan Timeline
        if (pauseTimelineDuringDialogue && director != null)
        {
            director.Play();
        }
    }


    // =========================================================
    // LOAD SCENE
    // =========================================================

    private void LoadTargetScene()
    {
        if (string.IsNullOrEmpty(targetSceneName))
        {
            Debug.LogError(
                "Target Scene belum dipilih!"
            );

            return;
        }


        if (!Application.CanStreamedLevelBeLoaded(targetSceneName))
        {
            Debug.LogError(
                $"Scene '{targetSceneName}' tidak dapat dibuka. " +
                "Pastikan Scene sudah dimasukkan ke Build Profiles > Scene List."
            );

            return;
        }


        Debug.Log(
            $"Pindah ke Scene: {targetSceneName}"
        );
        Time.timeScale = 1f;

        SceneManager.LoadScene(targetSceneName);
    }


    // =========================================================
    // EDITOR
    // =========================================================

#if UNITY_EDITOR

    private void OnValidate()
    {
        if (targetScene != null)
        {
            targetSceneName = targetScene.name;
        }
        else
        {
            targetSceneName = "";
        }
    }

#endif


    // =========================================================
    // CLEAN UP
    // =========================================================

    private void OnDisable()
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.OnDialogueFinished -=
                HandleDialogueFinished;
        }

        waitingForDialogue = false;
    }
}