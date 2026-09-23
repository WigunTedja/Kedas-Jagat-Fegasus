using System.Collections; // Tambahkan ini untuk Coroutine
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    // Tambahkan durasi animasi dan referensi ke script pergerakan
    [SerializeField] private float interactDuration = 0.5f;
    [SerializeField] private MonoBehaviour playerMovementScript;

    private GameObject objectInRange;
    private bool isCurrentlyInteracting = false; // Mencegah spam interaksi

    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.GetComponent<IInteractable>() != null)
        {
            objectInRange = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject == objectInRange)
        {
            objectInRange = null;
        }
    }

    void Update()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.eKey.isPressed && objectInRange != null && !isCurrentlyInteracting)
            {
                PerformInteraction();
            }
        }
    }

    public void OnMobileInteractButtonPressed()
    {
        if (objectInRange != null && !isCurrentlyInteracting)
        {
            PerformInteraction();
        }
    }

    private void PerformInteraction()
    {
        IInteractable interactable = objectInRange.GetComponent<IInteractable>();
        if (interactable != null)
        {
            StartCoroutine(InteractRoutine(interactable));
        }
    }

    private IEnumerator InteractRoutine(IInteractable interactable)
    {
        isCurrentlyInteracting = true;

        // 1. Hentikan pergerakan
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = false;
        }

        // Opsional: Jika menggunakan Rigidbody2D, paksa berhenti bergerak (hapus momentum)
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.linearVelocity = Vector2.zero; // Gunakan .velocity jika Unity versi lama
        }

        // 2. Jalankan interaksi & animasi
        _animator.SetTrigger("isInteracting");
        interactable.Interact(this.gameObject);
        AudioManager.Instance.PlayCollectSFX();

        // 3. Tunggu sampai animasi selesai
        yield return new WaitForSeconds(interactDuration);

        // 4. Kembalikan pergerakan
        if (playerMovementScript != null)
        {
            playerMovementScript.enabled = true;
        }

        isCurrentlyInteracting = false;
    }
}