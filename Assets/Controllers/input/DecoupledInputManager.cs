﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum GameInputMode
{
    Keyboard,
    Touch_1,
}

public class DecoupledInputManager : MonoBehaviour
{
    [SerializeField] public bool blockControl = false;
    [SerializeField] public GameInputMode Mode = GameInputMode.Keyboard;
    [SerializeField] KeyCode interact_key = KeyCode.E;
    [SerializeField] KeyCode jump_key = KeyCode.Space;

    [SerializeField] GameObject AssetBtnJump;
    TouchButton btn_jump
    {
        get
        {
            return AssetBtnJump.GetComponent<TouchButton>();
        }
    }
    [SerializeField] GameObject AssetBtnInteract;
    TouchButton btn_interact
    {
        get
        {
            return AssetBtnInteract.GetComponent<TouchButton>();
        }
    }
    [SerializeField] GameObject AssetJoystick;
    Joystick joystick
    {
        get
        {
            return AssetJoystick.GetComponent<Joystick>();
        }
    }

    public Vector2 touchInputVector = new Vector2(0, 0);

    void Start()
    {

    }

    void Update()
    {
        refreshInputVector();
    }


    private void refreshInputVector()
    {
        touchInputVector = new Vector2(0, 0);
        if (blockControl)
            return;

        switch (Mode)
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
        if (blockControl)
            return false;

        switch (Mode)
        {
            case GameInputMode.Keyboard:
                return Input.GetKeyDown(interact_key);
            case GameInputMode.Touch_1:
                return btn_interact.Pressed && !btn_interact.IsHeld; ;
            default:
                break;
        }

        return false;
    }

    public bool IsJumpKeyPressed()
    {
        if (blockControl)
            return false;

        switch (Mode)
        {
            case GameInputMode.Keyboard:
                return Input.GetKeyDown(jump_key);
            case GameInputMode.Touch_1:
                return btn_jump.Pressed && !btn_jump.IsHeld;
            default:
                break;
        }

        return false;
    }

    public Vector2 GetInputVector()
    {
        if (blockControl)
            return Vector2.zero;

        return touchInputVector;
    }

    public float GetAxis(string a_axis)
    {
        if (blockControl)
            return 0f;

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

