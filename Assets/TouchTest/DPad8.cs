using UnityEngine;
using UnityEngine.EventSystems;

public class DPad8 : MonoBehaviour, IDPad, IPointerDownHandler, IDragHandler, IPointerUpHandler
{
    public float Horizontal { get { return input.x; } }
    public float Vertical { get { return input.y; } }
    public Vector2 Direction { get { return new Vector2(Horizontal, Vertical); } }

    [SerializeField] private Vector2 input = Vector2.zero;
    [SerializeField] public RectTransform Up;
    [SerializeField] public RectTransform Down;
    [SerializeField] public RectTransform Left;
    [SerializeField] public RectTransform Right;
    [SerializeField] public RectTransform UR;
    [SerializeField] public RectTransform UL;
    [SerializeField] public RectTransform DR;
    [SerializeField] public RectTransform DL;

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
        bool ul = RectTransformUtility.RectangleContainsScreenPoint(
             UL, eventData.position, eventData.pressEventCamera
            );
        bool ur = RectTransformUtility.RectangleContainsScreenPoint(
             UR, eventData.position, eventData.pressEventCamera
             );
        bool dl = RectTransformUtility.RectangleContainsScreenPoint(
             DL, eventData.position, eventData.pressEventCamera
             );
        bool dr = RectTransformUtility.RectangleContainsScreenPoint(
             DR, eventData.position, eventData.pressEventCamera
             );

        //horiontal
        float h = 0f;
        float v = 0f;
        if (up)
            input = new Vector2(0, 1);
        if (down)
            input = new Vector2(0,-1);
        if (right)
            input = new Vector2(1, 0);
        if (left)
            input = new Vector2(-1,0);
        if (ur)
            input = new Vector2(1, 1);
        if (ul)
            input = new Vector2(-1, 1);
        if (dr)
            input = new Vector2(1, -1);
        if (dl)
            input = new Vector2(-1, -1);
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
