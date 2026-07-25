using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TrashCollectorUIDropZone : MonoBehaviour, IDropHandler
{
    public TrashCategory acceptedCategory;

    public Image binImage;

    public Sprite organicBin;
    public Sprite anorganicBin;
    public Sprite B3Bin;
    public Sprite keyHole;
    public void SetAcceptedCategory(TrashCategory category)
    {
        acceptedCategory = category;
        UpdateBinIcon();
    }

    public void UpdateBinIcon()
    {
        //if(binImage == null) { return; }

        switch (acceptedCategory)
        {
            case TrashCategory.Organik:
                binImage.sprite = organicBin;
                break;
            case TrashCategory.Anorganik:
                binImage.sprite = anorganicBin;
                break;
            case TrashCategory.B3:
                binImage.sprite = B3Bin;
                break;
            case TrashCategory.Key:
                binImage.sprite = keyHole;
                break;
            default:
                binImage = null; break;
        }
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void OnDrop(PointerEventData eventData)
    {
        if (eventData.pointerDrag != null)
        {
            InventorySlot draggedSlot = eventData.pointerDrag.GetComponent<InventorySlot>();

            if(draggedSlot != null && draggedSlot.item != null)
            {
                if(draggedSlot.item.category == acceptedCategory)
                {
                    Debug.Log("Sampah benar");
                    InventoryUI.Instance.inventory.Remove(draggedSlot.item);
                    InventoryUI.Instance.UpdateUI();
                    if(draggedSlot.item.category == TrashCategory.Key && acceptedCategory == TrashCategory.Key)
                    {
                        MinigameController.Instance.GateKeyProgress();
                    }
                }
                else
                {
                    Debug.Log("Sampah salah");
                }
            }
        }
    }
}
