using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Button : Interactable
{
    [SerializeField] private GameObject piston;
    [SerializeField] private float pushDepth = 0.1f;
    [SerializeField] private float pushDuration = 0.5f;
    private Vector3 initialPosition;
    bool inPushAnimation = false;
    float animTime = 0f;
    //public float lastToPush = 0f;

    public override void InitializeInteractable()
    {
        initialPosition = piston.transform.localPosition;
    }
    public override void UpdateInteractable()
    {
        float step = pushDepth*2/pushDuration;
        if (inPushAnimation)
        {
            if (animTime >= pushDuration)
            {
                HandleInteractableReset();
                return;
            }
            else
            {
                // very simple animation feel free to improve it
                float deltaTime = Time.deltaTime;
                animTime += deltaTime;
                float toPush = deltaTime * step;
                //lastToPush = toPush;

                if (animTime > pushDuration / 2)
                    toPush *= -1;

                piston.transform.Translate(0, 0, -toPush, Space.Self);
            }
        }
    }
    public override void HandleInteractableReset()
    {
        inPushAnimation = false;
        animTime = 0f;
        piston.transform.localPosition = initialPosition;
    }

    public override void ExecuteInteractionAnimation()
    {
        if (piston == null)
            return;
        if (inPushAnimation)
        {
            HandleInteractableReset();
        }
        inPushAnimation = true;
    }
}
