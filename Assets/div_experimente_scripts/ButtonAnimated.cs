using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonAnimated : Interactable
{
    //[SerializeField] private GameObject piston;
    //[SerializeField] private float pushDepth = 0.1f;
    //[SerializeField] private float pushDuration = 0.5f;
    //private Vector3 initialPosition;
    //bool inPushAnimation = false;
    //float animTime = 0f;
    //public float lastToPush = 0f;

    public override void InitializeInteractable()
    {
        //initialPosition = piston.transform.localPosition;
    }
    public override void UpdateInteractable()
    {
        ////float step = pushDepth * 2 / pushDuration;
        //if (inPushAnimation)
        //{
        //    //if (animTime >= pushDuration)
        //    //{
        //    //    HandleInteractableReset();
        //    //    return;
        //    //}
        //    //else
        //    //{
        //    //    // very simple animation feel free to improve it
        //    //    float deltaTime = Time.deltaTime;
        //    //    animTime += deltaTime;
        //    //    float toPush = deltaTime * step;
        //    //    //lastToPush = toPush;

        //    //    if (animTime > pushDuration / 2)
        //    //        toPush *= -1;

        //    //    piston.transform.Translate(0, 0, -toPush, Space.Self);
        //    //}
        //}
    }
    public override void HandleInteractableReset()
    {
        //inPushAnimation = false;
        //animTime = 0f;
        //piston.transform.localPosition = initialPosition;
    }

    public override void ExecuteInteractionAnimation()
    {
        //if (piston == null)
        //    return;
        var animator = GetComponent<Animator>();
        var foo = animator.GetCurrentAnimatorStateInfo(0);

        bool trig = animator.GetBool("press");
        Debug.Log("##btn press triggered: " + trig);
        if (!trig)
        {
            animator.SetTrigger("press");
        }
        else
        {
            // reset animation when btn is pressed while animation is still ongoing
            animator.Play("Idle");
            //animator.ResetTrigger("press");
            animator.SetTrigger("press");
        }
        //GetComponent<Animator>().ResetTrigger("press");
        //GetComponent<Animator>().SetTrigger("press2");




        //if (inPushAnimation)
        //{
        //    HandleInteractableReset();
        //}
        //inPushAnimation = true;
    }
}
