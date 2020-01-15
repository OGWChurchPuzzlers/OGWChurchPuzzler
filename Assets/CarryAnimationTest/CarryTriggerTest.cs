using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarryTriggerTest : MonoBehaviour
{
    [SerializeField] Animator characterAnimator;
    [SerializeField] string trigger;
    [SerializeField] bool state = false;

    public void SetAnimation(bool b)
    {
        state = b;
        characterAnimator.SetBool(trigger,state);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
