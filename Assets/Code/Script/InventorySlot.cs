using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.EventSystems;

public class InventorySlot : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public Image iconImage;
    public TMP_Text nameText;

    public Item item { get; private set; }

    private RectTransform rectTransform;
    public CanvasGroup canvasGroup;
    private Vector2 originalPosition;
    private Transform originalParent;

    private int originalSiblingIndex;

    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();
        canvasGroup = GetComponent<CanvasGroup>();
        //if(canvasGroup != null)
        //{
        //    canvasGroup = gameObject.AddComponent<CanvasGroup>();
        //}
    }

    public void AddItem(Item newItem)
    {
        item = newItem;

        iconImage.sprite = item.icon;
        iconImage.enabled = true;

        if(nameText != null)
        {
            nameText.text = item.name;
        }
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        if(item == null) { return; }
        originalPosition = rectTransform.anchoredPosition;
        originalParent = transform.parent;
        originalSiblingIndex = transform.GetSiblingIndex();

        //transform.SetParent(transform.root);
        transform.SetAsLastSibling();

        canvasGroup.alpha = 0.6f;
        canvasGroup.blocksRaycasts = false; //agar drop zone dapat mendeteksi kursor
    }

    public void OnDrag(PointerEventData eventData)
    {
        if(item == null) { return; }
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

    public void OnEndDrag(PointerEventData eventData)
    {
        if(item == null) { return; }
        canvasGroup.alpha = 1;
        canvasGroup.blocksRaycasts = true;

        // Kembalikan ke tempat semula (Jika berhasil di-drop, slot ini akan dihancurkan oleh UpdateUI)
        transform.SetParent(originalParent);
        transform.SetSiblingIndex(originalSiblingIndex);
        //rectTransform.anchoredPosition = originalPosition;
    }
}
