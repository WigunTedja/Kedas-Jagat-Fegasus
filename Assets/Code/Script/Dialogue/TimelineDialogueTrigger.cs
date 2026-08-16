using UnityEngine;
using UnityEngine.Playables;

public class TimelineDialogueTrigger : MonoBehaviour
{
    [Header("Timeline")]
    [SerializeField]
    private PlayableDirector director;

    [Header("Dialogue")]
    [SerializeField]
    private DialogueSO dialogue;

    private bool waitingForDialogue;

    public void PlayDialogue()
    {
        if (waitingForDialogue)
            return;

        if (dialogue == null)
        {
            Debug.LogWarning(
                "Dialogue belum dimasukkan."
            );

            return;
        }

        if (DialogueManager.Instance == null)
        {
            Debug.LogError(
                "DialogueManager tidak ditemukan."
            );

            return;
        }

        waitingForDialogue = true;

        // Pause cutscene.
        if (director != null)
        {
            director.Pause();
        }

        // Dengarkan ketika dialogue selesai.
        DialogueManager.Instance.OnDialogueFinished +=
            ContinueTimeline;

        // Jalankan dialogue.
        DialogueManager.Instance.StartDialogue(
            dialogue
        );
    }

    private void ContinueTimeline()
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.OnDialogueFinished -=
                ContinueTimeline;
        }

        waitingForDialogue = false;

        // Lanjutkan cutscene.
        if (director != null)
        {
            director.Play();
        }
    }

    private void OnDisable()
    {
        if (DialogueManager.Instance != null)
        {
            DialogueManager.Instance.OnDialogueFinished -=
                ContinueTimeline;
        }
    }
}