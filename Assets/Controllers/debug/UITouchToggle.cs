using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITouchToggle : MonoBehaviour
{
    [SerializeField] DecoupledInputManager inputManager;
    [SerializeField] GameObject TouchJoystick;
    [SerializeField] GameObject TouchButtons;

    public void ToggleTouchInput(bool a_useTouch)
    {
        if (a_useTouch)
        {
            TouchJoystick?.SetActive(true);
            TouchButtons?.SetActive(true);
            inputManager.Mode = GameInputMode.Touch_1;
        }
        else
        {
            TouchJoystick?.SetActive(false);
            TouchButtons?.SetActive(false);
            inputManager.Mode = GameInputMode.Keyboard;
        }
    }
}
