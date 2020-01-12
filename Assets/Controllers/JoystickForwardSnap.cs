using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JoystickForwardSnap : Joystick
{
    [SerializeField] private float m_Angle = 0f;
    public SegmentedJoystickMode mode = SegmentedJoystickMode.DPad4;

    // HACK
    protected override void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
    {
        m_Angle = Vector2.SignedAngle(normalised, new Vector2(0, 1));

                float forward_lockAngle = 7.5f; // 5
                if (inBetween(m_Angle, 0, forward_lockAngle) || m_Angle == 0 || inBetween(m_Angle, -forward_lockAngle, 0))
                {
                    //Debug.Log("T");
                    normalised = new Vector2(0, 1);
                }

                //Debug.Log("angle: " +m_Angle);
                //Debug.Log("magnitude: " + magnitude);
                if (magnitude < 1)
                {
                    normalised = normalised * magnitude;
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