using UnityEngine;

public class InventoryUI: MonoBehaviour
{
    public static InventoryUI Instance { get; private set; }

    public GameObject inventoryPanel;
    public Transform itemsParent;
    public GameObject slotPrefab;
    public Inventory inventory;

    //Trash collecotr UI
    public GameObject trashCollectorDropZoneUI;
    public GameObject keyCollectorDropZoneUI;
    public TrashCollectorUIDropZone dropZoneScript;
    
    void Awake()
    {
        if (Instance == null) { Instance = this; }
        else { Destroy(gameObject); }
    }
    void Start()
    {
        inventoryPanel.SetActive(false);
        if(trashCollectorDropZoneUI != null)
        {
            trashCollectorDropZoneUI.SetActive(false);
        }
    }

    // Update is called once per frame
    public void ToggleInventory()
    {
        if (trashCollectorDropZoneUI != null) trashCollectorDropZoneUI.SetActive(false);

        inventoryPanel.SetActive(!inventoryPanel.activeSelf);
        if (inventoryPanel.activeSelf)
        {
            UpdateUI();
        }
    }

    public void OpenForTrashCollection(TrashCategory acceptedCategory)
    {
        inventoryPanel.SetActive(true);

        if(trashCollectorDropZoneUI != null && dropZoneScript != null)
        {
            trashCollectorDropZoneUI.SetActive(true);
            dropZoneScript.SetAcceptedCategory(acceptedCategory);
        }
        UpdateUI();
    }
    public void OpenForKeyCollection(TrashCategory acceptedCategory)
    {
        inventoryPanel.SetActive(true);

        if (keyCollectorDropZoneUI != null && dropZoneScript != null)
        {
            keyCollectorDropZoneUI.SetActive(true);
            dropZoneScript.SetAcceptedCategory(acceptedCategory);
        }
        UpdateUI();
    }

    public void UpdateUI()
    {
        foreach (Transform child in itemsParent)
        {
            Destroy(child.gameObject);
        }
        foreach(Item item in inventory.items)
        {
            GameObject newSlot = Instantiate(slotPrefab, itemsParent);
            InventorySlot slotScript = newSlot.GetComponent<InventorySlot>();

            if(slotScript != null)
            {
                slotScript.AddItem(item);
            }
        }
    }
}
