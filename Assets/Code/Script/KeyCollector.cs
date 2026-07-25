using UnityEngine;

public class KeyCollector : MonoBehaviour, IInteractable
{
    public TrashCategory collectorCategory = TrashCategory.Key;
    public void Interact(GameObject interactor)
    {
        Debug.Log("Player interacted with trCollecter");
        Inventory inventory = interactor.GetComponent<Inventory>();
        if (inventory != null)
        {
            Debug.Log("Found the inventory!");
            if (InventoryUI.Instance != null)
            {
                InventoryUI.Instance.OpenForTrashCollection(collectorCategory);
            }
            else
            {
                Debug.Log("InventoryUI tidak ditemukan");
            }
        }
        else
        {
            Debug.Log("Interactor has no Inventory");
        }
    }
}
