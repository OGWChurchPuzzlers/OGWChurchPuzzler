using UnityEngine;
using UnityEngine.EventSystems;

public class TouchButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [HideInInspector]
    public bool Pressed = false;
    private int pressedAt = 0;
    public bool IsHeld // nur semi gute lösung! -> muss auf event oder ähnliches geändert werden
    {
        get
        {
            if (Pressed && pressedAt != 0 && pressedAt < Time.frameCount)
                return true;

            return false;
        }
    }

    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        pressedAt = Time.frameCount;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        pressedAt = 0;
    }
}
