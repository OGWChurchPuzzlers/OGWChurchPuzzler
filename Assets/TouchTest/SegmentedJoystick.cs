using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum SegmentedJoystickMode
{
    DPad4,
    DPad8,
    Joystick_ForwardHelp,

}
/// <summary>
///  HACK
/// </summary>
public class SegmentedJoystick : Joystick
{
    [SerializeField] private float m_Angle = 0f;
    public SegmentedJoystickMode mode = SegmentedJoystickMode.DPad4;

    // HACK
    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        //if (joystickType == JoystickType.Dynamic && magnitude > moveThreshold)
        //{
        //    Vector2 difference = normalised * (magnitude - moveThreshold) * radius;
        //    background.anchoredPosition += difference;
        //}
        m_Angle = Vector2.SignedAngle(normalised, new Vector2(0,1));

        switch (mode)
        {
            case SegmentedJoystickMode.DPad4:
                // 4
                if (inBetween(m_Angle, 0, 45) || m_Angle == 0 || inBetween(m_Angle, -45, 0))
                {
                    Debug.Log("T");
                    normalised = new Vector2(0, 1);
                }
                if (inBetween(m_Angle, 135, 180) || m_Angle == 180 || inBetween(m_Angle, -180, -135))
                {
                    Debug.Log("D");
                    normalised = new Vector2(0, -1);
                }
                if (inBetween(m_Angle, 45, 135))
                {
                    Debug.Log("R");
                    normalised = new Vector2(1, 0);
                }
                if (inBetween(m_Angle, -135, -45))
                {
                    Debug.Log("L");
                    normalised = new Vector2(-1, 0);
                }
                if (magnitude < 1)
                {
                    normalised = normalised * magnitude;
                }
                break;
            case SegmentedJoystickMode.DPad8:
                // 8
                if (inBetween(m_Angle, 0, 22.5f) || m_Angle == 0 || inBetween(m_Angle, -22.5f, 0))
                {
                    Debug.Log("T");
                    normalised = new Vector2(0, 1);
                }
                if (inBetween(m_Angle, 157.5f, 180) || m_Angle == 180 || inBetween(m_Angle, -180, -157.5f))
                {
                    Debug.Log("D");
                    normalised = new Vector2(0, -1);
                }
                if (inBetween(m_Angle, 67.5f, 112.5f))
                {
                    Debug.Log("R");
                    normalised = new Vector2(1, 0);
                }
                if (inBetween(m_Angle, -112.5f, -67.5f))
                {
                    Debug.Log("L");
                    normalised = new Vector2(-1, 0);
                }
                if (inBetween(m_Angle, 22.5f, 67.5f))
                {
                    Debug.Log("TR");
                    normalised = new Vector2(1, 1).normalized;
                }
                if (inBetween(m_Angle, -67.5f, -22.5f))
                {
                    Debug.Log("TL");
                    normalised = new Vector2(-1, 1).normalized;
                }
                if (inBetween(m_Angle, 112.5f, 157.5f))
                {
                    Debug.Log("BR");
                    normalised = new Vector2(1, -1).normalized;
                }
                if (inBetween(m_Angle, -157.5f, -112.5f))
                {
                    Debug.Log("BL");
                    normalised = new Vector2(-1, -1).normalized;
                }
                if (magnitude < 1)
                {
                    normalised = normalised * magnitude;
                }
                break;
            case SegmentedJoystickMode.Joystick_ForwardHelp:
                //// 2
                float forward_lockAngle = 7.5f; // 5
                if (inBetween(m_Angle, 0, forward_lockAngle) || m_Angle == 0 || inBetween(m_Angle, -forward_lockAngle, 0))
                {
                    Debug.Log("T");
                    normalised = new Vector2(0, 1);
                }

                //Debug.Log("angle: " +m_Angle);
                Debug.Log("magnitude: " + magnitude);
                if (magnitude < 1)
                {
                    normalised = normalised * magnitude;
                }
                break;
            default:
                break;
        }
        base.HandleInput(2.0f, normalised, radius, cam);
        bool inBetween(float a, float c, float d)
        {
            float max = System.Math.Max(c, d);
            float min = System.Math.Min(c, d);

            return a < max && a > min;
        }
    }
}
