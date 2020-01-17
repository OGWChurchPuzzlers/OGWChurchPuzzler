using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Seil : Interactable
{
    [SerializeField] Animator m_animator;
    [SerializeField] string m_animationTrigger = "";
    //[SerializeField] string m_defaultState = "";

    public override void ExecuteInteractionAnimation()
    {
        Debug.Log("Seil Trigger");
        if(m_animator != null)
        {
            //if (m_animator.GetBool(m_animationTrigger))
            //{
            //    Debug.Log("resetting");
            //    m_animator.Play(m_defaultState);
            //}
            m_animator.SetTrigger(m_animationTrigger);
        }
    }

    public override void HandleInteractableReset()
    {
    }

    public override void InitializeInteractable()
    {
    }

    public override void UpdateInteractable()
    {
    }


}
