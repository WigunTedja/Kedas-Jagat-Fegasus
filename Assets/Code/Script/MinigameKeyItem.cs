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

        InventoryUI.Instance.UpdateUI();

        MinigameController.Instance.CloseActiveMinigame();

        Destroy(minigameTrigger);
    }
}
