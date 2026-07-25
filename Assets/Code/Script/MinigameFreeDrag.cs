using UnityEngine;
using UnityEngine.EventSystems;

// Tempelkan script ini pada UI Image Daun Kering
public class MinigameFreeDrag : MonoBehaviour, IDragHandler
{
    private RectTransform rectTransform;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition;
        Vector2 clampedPosition = eventData.position;

        clampedPosition.x = Mathf.Clamp(clampedPosition.x, 0f, Screen.width);
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, 0f, Screen.height);

        // Menggunakan logika konversi posisi yang sama dengan MinigameDraggableItem
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            (RectTransform)rectTransform.parent,
            clampedPosition,
            eventData.pressEventCamera,
            out mousePosition))
        {
            rectTransform.position = mousePosition; // Daun bebas digeser ke mana saja
        }
    }
}