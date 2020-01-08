using UnityEngine;
using UnityEngine.EventSystems;

public interface IDPad
{
    float Horizontal { get; }
    float Vertical { get; }
    Vector2 Direction { get; }
}

public class DPad4 : MonoBehaviour, IDPad, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float Horizontal { get { return input.x; } }
    public float Vertical { get { return input.y; } }
    public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }

    private Vector2 input = Vector2.zero;
    [SerializeField] public RectTransform Up;
    [SerializeField] public RectTransform Down;
    [SerializeField] public RectTransform Left;
    [SerializeField] public RectTransform Right;

    [SerializeField] public bool FingerInDPad = false;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    public void OnPointerDown(PointerEventData eventData)
    {
        HandleEventData(eventData);
        FingerInDPad = true;
    }
    public void OnDrag(PointerEventData eventData)
    {
        HandleEventData(eventData);
        FingerInDPad = true;
    }

    private void HandleEventData(PointerEventData eventData)
    {
        input = Vector2.zero;

        bool up = RectTransformUtility.RectangleContainsScreenPoint(
             Up, eventData.position, eventData.pressEventCamera
             );
        bool down = RectTransformUtility.RectangleContainsScreenPoint(
             Down, eventData.position, eventData.pressEventCamera
             );
        bool left = RectTransformUtility.RectangleContainsScreenPoint(
             Left, eventData.position, eventData.pressEventCamera
             );
        bool right = RectTransformUtility.RectangleContainsScreenPoint(
             Right, eventData.position, eventData.pressEventCamera
             );
        //horiontal
        float h = 0f;
        float v = 0f;
        if(!(up && down))
        {
            if (up)
                v = 1;
            if (down)
                v = -1;
        }
        if (!(right && left))
        {
            if (right)
                h = 1;
            if (left)
                h = -1;
        }
        input = new Vector2(h, v);
    }
    /**
     *  if (RectTransformUtility.RectangleContainsScreenPoint(
             Up, eventData.position, eventData.pressEventCamera
             ))
            {
                //targetRectArea is the rectangle transform you want your
                //input to be inside, the above will return true if it is inside.
                touchRecognized = true;
            }
            else
            {
                touchRecognized = false;
            }
    */

    public void OnPointerUp(PointerEventData eventData)
    {
        FingerInDPad = false;
        input = Vector2.zero;
    }


}


