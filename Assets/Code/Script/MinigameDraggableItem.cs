using UnityEngine;
using UnityEngine.EventSystems;

public class MinigameDraggableItem : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public enum TrashCategory { Organik, Anorganik, B3}
    public TrashCategory trashCategory;

    private RectTransform rectTransform;
    private CanvasGroup canvasGroup;
    private Canvas parentCanvas;
    private Vector2 originalPosition;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        originalPosition = rectTransform.anchoredPosition;
        canvasGroup = GetComponent<CanvasGroup>();
        parentCanvas = GetComponentInParent<Canvas>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        //jika salah tong maka bisa kembali ke posisi awal
        //originalPosition = rectTransform.anchoredPosition;

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false;
        transform.SetAsLastSibling();
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector3 mousePosition;
        // 1. Ambil posisi asli mouse di layar
        Vector2 clampedPosition = eventData.position;

        // 2. Batasi sumbu X (kiri ke kanan)
        // 0 adalah ujung kiri layar, Screen.width adalah ujung kanan layar
        clampedPosition.x = Mathf.Clamp(clampedPosition.x, 0f, Screen.width);

        // 3. Batasi sumbu Y (bawah ke atas)
        // 0 adalah ujung bawah layar, Screen.height adalah ujung atas layar
        clampedPosition.y = Mathf.Clamp(clampedPosition.y, 0f, Screen.height);
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            (RectTransform)rectTransform.parent, // Menggunakan area dari parent (panel)
            clampedPosition,                  // Posisi mouse di layar
            eventData.pressEventCamera,          // Kamera yang membaca klik UI
            out mousePosition))                  // Hasil posisi yang sudah dikonversi
        {
            // Pindahkan sampah langsung ke titik mouse tersebut
            rectTransform.position = mousePosition;
        }
    }

    //public void OnDrag(PointerEventData eventData)
    //{
    //    rectTransform.anchoredPosition += eventData.delta; /// parentCanvas.scaleFactor;
    //}

    public void OnEndDrag(PointerEventData eventData)
    {
        canvasGroup.alpha = 1f;
        canvasGroup.blocksRaycasts = true;
    }

    public void ResetPosition()
    {
        rectTransform.anchoredPosition = originalPosition;
    }
}
