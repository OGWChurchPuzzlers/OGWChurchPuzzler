using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

enum DecoupledInputMode
{
    Keyboard = 0,
    TouchDPad = 1,
    Joystick_fixed = 2,
    Joystick_floating = 4,
    Joystick_dynamic = 8,
    Joystick_default = 16,

}
public class DecoupledControllerInputTest : MonoBehaviour
{
    [SerializeField] DecoupledInputMode mode = DecoupledInputMode.Keyboard;
    [SerializeField] GameObject AssetJoystick;
    [SerializeField] VariableJoystick joystick
    {
        get
        {
            return AssetJoystick.GetComponent<VariableJoystick>();
        }
    }
    [SerializeField] Dropdown dropdown;
    [SerializeField] GameObject JoystickDefault_GO;
    [SerializeField] SegmentedJoystick joystick_default
    {
        get
        {
            return JoystickDefault_GO.GetComponent<SegmentedJoystick>();
        }
    }

    [SerializeField] GameObject DPad_GO;
   
    IDPad dpad
    {
        get
        {
            return DPad_GO.GetComponent<IDPad>();
        }
    }

    public Vector2 touchInputVector = new Vector2(0, 0);

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
      

        // TODO nachforschen wo/wann am besten refresh stattfinden sollte. erst bei update() oder schon früher? 
        refreshInputVector();
    }
    public void ModeChanged(int index)
    {
        // HACK nur für demo
        int dropdownMode = index;// dropdown.value;
        switch (dropdownMode)
        {
            case 0:
                mode = DecoupledInputMode.Keyboard;
                AssetJoystick.SetActive(false);
                DPad_GO.SetActive(false);
                JoystickDefault_GO.SetActive(false);
                break;
            case 1:
                mode = DecoupledInputMode.TouchDPad;
                AssetJoystick.SetActive(false);
                DPad_GO.SetActive(true);
                JoystickDefault_GO.SetActive(false);
                break;
            case 2:
                mode = DecoupledInputMode.Joystick_fixed;
                AssetJoystick.SetActive(true);
                DPad_GO.SetActive(false);
                joystick.SetMode(JoystickType.Fixed);
                JoystickDefault_GO.SetActive(false);
                break;
            case 3:
                mode = DecoupledInputMode.Joystick_floating;
                AssetJoystick.SetActive(true);
                DPad_GO.SetActive(false);
                joystick.SetMode(JoystickType.Floating);
                JoystickDefault_GO.SetActive(false);
                break;
            case 4:
                mode = DecoupledInputMode.Joystick_dynamic;
                AssetJoystick.SetActive(true);
                DPad_GO.SetActive(false);
                joystick.SetMode(JoystickType.Dynamic);
                JoystickDefault_GO.SetActive(false);
                break;
            case 5:
                mode = DecoupledInputMode.Joystick_default;
                AssetJoystick.SetActive(false);
                DPad_GO.SetActive(false);
                JoystickDefault_GO.SetActive(true);
                joystick_default.mode = SegmentedJoystickMode.Joystick_ForwardHelp;
                break;
            case 6:
                mode = DecoupledInputMode.Joystick_default;
                AssetJoystick.SetActive(false);
                DPad_GO.SetActive(false);
                JoystickDefault_GO.SetActive(true);
                joystick_default.mode = SegmentedJoystickMode.DPad4;
                break;
            case 7:
                mode = DecoupledInputMode.Joystick_default;
                AssetJoystick.SetActive(false);
                DPad_GO.SetActive(false);
                JoystickDefault_GO.SetActive(true);
                joystick_default.mode = SegmentedJoystickMode.DPad8;
                break;
            default:
                break;
        }
    }

    private void refreshInputVector()
    {
        touchInputVector = new Vector2(0, 0);
        switch (mode)
        {
            case DecoupledInputMode.Keyboard:
                    touchInputVector.x = Input.GetAxis("Horizontal");
                    touchInputVector.y = Input.GetAxis("Vertical");
                break;
            case DecoupledInputMode.TouchDPad:
                touchInputVector = dpad.Direction;
                break;
            case DecoupledInputMode.Joystick_fixed:
            case DecoupledInputMode.Joystick_floating:
            case DecoupledInputMode.Joystick_dynamic:
                touchInputVector = joystick.Direction;
                break;
            case DecoupledInputMode.Joystick_default:
                touchInputVector = joystick_default.Direction;
                break;
            default:
                break;
        }
        //if (L.buttonPressed && R.buttonPressed)
        //{
        //    touchInputVector.x = 0;
        //}
        //else
        //{
        //    if (L.buttonPressed)
        //    {
        //        touchInputVector.x = -1;
        //    }
        //    if (R.buttonPressed)
        //    {
        //        touchInputVector.x = 1;
        //    }
        //}
        //if (V.buttonPressed && B.buttonPressed)
        //{
        //    touchInputVector.y = 0;
        //}
        //else
        //{
        //    if (V.buttonPressed)
        //    {
        //        touchInputVector.y = 1;
        //    }
        //    if (B.buttonPressed)
        //    {
        //        touchInputVector.y = -1;
        //    }
        //}
    }

    public Vector2 GetInputVector()
    {
         //refreshInputVector();
        return touchInputVector;
    }

    public float GetAxis(string a_axis)
    {
        //refreshInputVector(); // TODO woanders aufrufen - eigentlich schlecht wenn immer bei Get refresht wird
        
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
