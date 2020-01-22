using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TouchButton : MonoBehaviour, IPointerUpHandler, IPointerDownHandler
{
    [HideInInspector]
    public bool Pressed = false;
    private int pressedAt = 0;
    private Color unpressedColor;
    [SerializeField] Color pressedColor = Color.black;
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
        unpressedColor = this.GetComponent<Image>().color;
    }

    // Update is called once per frame
    void Update()
    {
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Pressed = true;
        pressedAt = Time.frameCount;
        this.GetComponent<Image>().color = pressedColor;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        Pressed = false;
        pressedAt = 0;
        this.GetComponent<Image>().color = unpressedColor;
    }

    public void DebugOverride_SetPressStatus(bool a_pressed)
    {
        if (a_pressed)
        {
            this.GetComponent<Image>().color = pressedColor;

        }
        else
        {
            this.GetComponent<Image>().color = unpressedColor;
        }
    }
}
