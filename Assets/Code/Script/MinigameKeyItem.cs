using UnityEngine;
using UnityEngine.EventSystems;

public class MinigameKeyItem : MonoBehaviour, IPointerClickHandler
{
    public Item keyItemData;

    [Header("Corresponding minigame trigger")]
    public GameObject minigameTrigger;
    public void OnPointerClick(PointerEventData eventData)
    {
        Debug.Log("Kunci berhasil ditemukan!");

        InventoryUI.Instance.inventory.Add(keyItemData);
        string itemName = keyItemData.ItemName.ToLower();
        if (itemName.Contains("trash") || itemName.Contains("sampah"))
        {
            LevelManager.Instance.totalTrashInLevel += 1;
        }

        InventoryUI.Instance.UpdateUI();

        MinigameController.Instance.CloseActiveMinigame();

        Destroy(minigameTrigger);
    }
}
