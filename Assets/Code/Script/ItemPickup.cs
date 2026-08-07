using UnityEngine;

public class ItemPickUp : MonoBehaviour, IInteractable
{
    public Item ItemData;

    public void Interact(GameObject interactor)
    {
        Inventory inventory = interactor.GetComponent<Inventory>();

        if(inventory != null )
        {
            bool wasPickedUp = inventory.Add(ItemData);
            if(wasPickedUp)
            {
                Destroy(gameObject);
            }
        }
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    Inventory inventory = collision.GetComponent<Inventory>();

    //    if(inventory != null)
    //    {
    //        bool WasPickedUp = inventory.Add(ItemData);
    //        if (WasPickedUp)
    //        {
    //            Destroy(gameObject);
    //        }
    //    }
    //}
}
