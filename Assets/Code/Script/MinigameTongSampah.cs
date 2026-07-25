using UnityEngine;
using UnityEngine.EventSystems;

public class MinigameTongSampah : MonoBehaviour, IDropHandler
{
    public MinigameDraggableItem.TrashCategory acceptedCategory;
    public void OnDrop(PointerEventData eventData)
    {
        //cek ada objek drop di sini
        if(eventData.pointerDrag != null)
        {
            MinigameDraggableItem droppedItem = eventData.pointerDrag.GetComponent<MinigameDraggableItem>();
            if(droppedItem != null)
            {
                if(droppedItem.trashCategory == acceptedCategory)
                {
                    Debug.Log("Sampah benar, sampah masuk");
                    Destroy(droppedItem.gameObject);
                }
                else
                {
                    Debug.Log("Sampah salah, mengembalikan sampah");
                    droppedItem.ResetPosition();
                }
            }
        }
    }
}
