using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    private GameObject objectInRange;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Check if the thing we just bumped into signed the contract
        if (collision.GetComponent<IInteractable>() != null)
        {
            objectInRange = collision.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // If we walk away from the object we were tracking, clear it out
        if (collision.gameObject == objectInRange)
        {
            objectInRange = null;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Keyboard.current != null)
        {
            if (Keyboard.current.eKey.isPressed && objectInRange != null){
                PerformInteraction();
            }
        }
    }

    public void OnMobileInteractButtonPressed()
    {
        if (objectInRange != null)
        {
            PerformInteraction();
        }
    }

    private void PerformInteraction()
    {
        IInteractable interactable = objectInRange.GetComponent<IInteractable>();
        if (interactable != null)
        {
            _animator.SetTrigger("isInteracting");
            interactable.Interact(this.gameObject);
            AudioManager.Instance.PlayCollectSFX();
        }
    }
}
