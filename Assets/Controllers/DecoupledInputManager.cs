using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum GameInputMode
{
    Keyboard,
    Touch_1,
}

public class DecoupledInputManager : MonoBehaviour
{
    [SerializeField] bool blockControl = false;
    [SerializeField] GameInputMode mode = GameInputMode.Keyboard;
    [SerializeField] GameObject JoystickAsset;
    [SerializeField] Joystick joystick
    {
        get
        {
            return JoystickAsset.GetComponent<Joystick>();
        }
    }

    public Vector2 touchInputVector = new Vector2(0, 0);

    void Start()
    {

    }

    void Update()
    {
        // TODO nachforschen wo/wann am besten refresh stattfinden sollte. erst bei update() oder schon früher? 
        refreshInputVector();
    }
   

    private void refreshInputVector()
    {
        touchInputVector = new Vector2(0, 0);
        switch (mode)
        {
            case GameInputMode.Keyboard:
                touchInputVector.x = Input.GetAxis("Horizontal");
                touchInputVector.y = Input.GetAxis("Vertical");
                break;
            case GameInputMode.Touch_1:
                touchInputVector = joystick.Direction;
                break;
            default:
                break;
        }
    }

    public bool IsInteractionKeyPressed()
    {
        //TODO
        if (Input.GetKeyDown(KeyCode.E))
        {
            return true;
        }

        return false;
    }

    public Vector2 GetInputVector()
    {
        if (blockControl)
        {
            return Vector2.zero;
        }
        return touchInputVector;
    }

    public float GetAxis(string a_axis)
    {
        if (blockControl)
        {
            return 0f;
        }

        switch (a_axis)
        {
            case "Horizontal":
                return touchInputVector.x;
            case "Vertical":
                return touchInputVector.y;
            default:
                break;
        }
        return 0f;
    }
}

