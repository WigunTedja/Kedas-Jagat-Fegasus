using System.Collections;
using UnityEngine;
using UnityEngine.Playables;

public class TimelineDialogueTrigger : MonoBehaviour
{
    [Header("Dialogue")]
    [SerializeField]
    private DialogueSO dialogue;

    [Header("Auto Start")]
    [Tooltip("Jika aktif, dialogue akan otomatis muncul ketika scene dimulai.")]
    [SerializeField]
    private bool playOnSceneStart = false;

    [Tooltip("Delay sebelum dialogue otomatis dimulai.")]
    [SerializeField]
    private float startDelay = 0f;

    [Header("Timeline")]
    [Tooltip("Boleh dikosongkan jika dialogue tidak berhubungan dengan Timeline.")]
    [SerializeField]
    private PlayableDirector director;

    [Tooltip("Pause Timeline selama dialogue berlangsung.")]
    [SerializeField]
    private bool pauseTimelineDuringDialogue = true;

    private bool waitingForDialogue = false;

    private void Start()
    {
        // Hanya otomatis jika pilihan ini diaktifkan.
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

    // Fungsi ini tetap bisa dipanggil dari Timeline Signal.
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

        // Jangan mulai dialogue baru jika sedang ada dialogue.
        if (DialogueManager.Instance.IsDialogueActive)
        {
            Debug.LogWarning(
                "Dialogue lain sedang berjalan."
            );

            return;
        }

        waitingForDialogue = true;

        // Pause Timeline jika memang menggunakan Timeline.
        if (pauseTimelineDuringDialogue && director != null)
        {
            director.Pause();
        }

        DialogueManager.Instance.OnDialogueFinished +=
            HandleDialogueFinished;

        DialogueManager.Instance.StartDialogue(dialogue);
    }

    private void HandleDialogueFinished()
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.OnDialogueFinished -=
                HandleDialogueFinished;
        }

        waitingForDialogue = false;

        // Lanjutkan Timeline.
        if (pauseTimelineDuringDialogue && director != null)
        {
            director.Play();
        }
    }

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