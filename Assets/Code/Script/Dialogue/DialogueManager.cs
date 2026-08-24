using System;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class DialogueManager : MonoBehaviour
{
    public static DialogueManager Instance { get; private set; }

    [Header("Dialogue UI")]
    [SerializeField] private GameObject dialoguePanel;
    [SerializeField] private TMP_Text speakerNameText;
    [SerializeField] private TMP_Text dialogueText;
    [SerializeField] private Image portraitImage;

    [Header("Typing")]
    [SerializeField] private float typingSpeed = 0.03f;

    private DialogueSO currentDialogue;

    private int currentLineIndex;

    private bool isTyping;
    private bool dialogueActive;

    private string currentFullText;

    private Coroutine typingCoroutine;

    public bool IsDialogueActive => dialogueActive;

    public event Action OnDialogueFinished;

    private void Awake()
    {
        // Singleton sederhana untuk satu DialogueManager di scene.
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }
    }

    private void Update()
    {
        if (!dialogueActive)
            return;

        bool nextPressed = false;

        // Keyboard
        if (Keyboard.current != null)
        {
            if (Keyboard.current.spaceKey.wasPressedThisFrame ||
                Keyboard.current.enterKey.wasPressedThisFrame)
            {
                nextPressed = true;
            }
        }

        //Mobile
        if (Touchscreen.current != null)
        {
            if (Touchscreen.current.wasUpdatedThisFrame)
            {
                nextPressed = true;
            }
        }
        // Mouse
        if (Mouse.current != null)
        {
            if (Mouse.current.leftButton.wasPressedThisFrame)
            {
                nextPressed = true;
            }
        }

        // Gamepad
        if (Gamepad.current != null)
        {
            if (Gamepad.current.buttonSouth.wasPressedThisFrame)
            {
                nextPressed = true;
            }
        }

        if (nextPressed)
        {
            Next();
        }
    }

    public void StartDialogue(DialogueSO dialogue)
    {
        if (dialogue == null)
        {
            Debug.LogWarning("DialogueSO kosong.");
            return;
        }

        if (dialogue.lines == null || dialogue.lines.Length == 0)
        {
            Debug.LogWarning(
                $"Dialogue '{dialogue.name}' tidak memiliki dialogue line."
            );

            return;
        }

        currentDialogue = dialogue;
        currentLineIndex = 0;

        dialogueActive = true;

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(true);
        }

        ShowCurrentLine();
    }

    private void ShowCurrentLine()
    {
        DialogueLine line =
            currentDialogue.lines[currentLineIndex];

        // =========================
        // Speaker
        // =========================

        if (line.speaker != null)
        {
            speakerNameText.text =
                line.speaker.actorName;

            if (portraitImage != null)
            {
                portraitImage.sprite =
                    line.speaker.portrait;

                portraitImage.gameObject.SetActive(
                    line.speaker.portrait != null
                );
            }
        }
        else
        {
            // Bisa dipakai untuk Narator tanpa portrait.
            speakerNameText.text = "";

            if (portraitImage != null)
            {
                portraitImage.gameObject.SetActive(false);
            }
        }

        // =========================
        // Text
        // =========================

        currentFullText = line.text;

        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);
        }

        typingCoroutine =
            StartCoroutine(TypeText(currentFullText));
    }

    private IEnumerator TypeText(string text)
    {
        isTyping = true;

        dialogueText.text = "";

        foreach (char character in text)
        {
            dialogueText.text += character;

            yield return new WaitForSecondsRealtime(
                typingSpeed
            );
        }

        dialogueText.text = text;

        isTyping = false;

        typingCoroutine = null;
    }

    private void Next()
    {
        // Kalau masih mengetik,
        // satu kali klik langsung menyelesaikan teks.
        if (isTyping)
        {
            CompleteCurrentText();
            return;
        }

        currentLineIndex++;

        // Dialogue selesai.
        if (currentLineIndex >= currentDialogue.lines.Length)
        {
            EndDialogue();
            return;
        }

        ShowCurrentLine();
    }

    private void CompleteCurrentText()
    {
        if (typingCoroutine != null)
        {
            StopCoroutine(typingCoroutine);

            typingCoroutine = null;
        }

        dialogueText.text = currentFullText;

        isTyping = false;
    }

    private void EndDialogue()
    {
        dialogueActive = false;

        currentDialogue = null;

        currentLineIndex = 0;

        if (dialoguePanel != null)
        {
            dialoguePanel.SetActive(false);
        }

        OnDialogueFinished?.Invoke();
    }
}