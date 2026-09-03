using UnityEngine;
using UnityEngine.EventSystems;

public class MobileJoystick : MonoBehaviour, IDragHandler, IPointerUpHandler, IPointerDownHandler
{
    public RectTransform joystickKnob;
    public Vector2 InputVector { get; private set; }
    private float maxRadius;

    void Start()
    {
        // Menentukan seberapa jauh knob bisa ditarik
        maxRadius = GetComponent<RectTransform>().sizeDelta.x / 2f;
    }

    public void OnDrag(PointerEventData eventData)
    {
        Vector2 position;
        RectTransformUtility.ScreenPointToLocalPointInRectangle(
            GetComponent<RectTransform>(), eventData.position, eventData.pressEventCamera, out position);

        // Membatasi pergerakan knob agar tidak keluar batas
        InputVector = Vector2.ClampMagnitude(position, maxRadius) / maxRadius;
        joystickKnob.anchoredPosition = InputVector * maxRadius;
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        OnDrag(eventData);
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        InputVector = Vector2.zero;
        joystickKnob.anchoredPosition = Vector2.zero;
    }
}