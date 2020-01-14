using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lichtschalter : Interactable
{
    public bool inOnPosition = false;
    [SerializeField] private GameObject hebel;
    //private Quaternion intialRoation;

    public override void ExecuteInteractionAnimation()
    {
        if (inOnPosition)
        {
            //Debug.Log("## Lichtschalter On");
            hebel.transform.Rotate(0, 90, 0, Space.Self);
            //hebel.transform.localRotation = intialRoation;
            inOnPosition = false;
        }
        else
        {
            //Debug.Log("## Lichtschalter Off");
            hebel.transform.Rotate(0, -90, 0,Space.Self);
            inOnPosition = true;
        }
    }

    public override void HandleInteractableReset()
    {
        //hebel.transform.localRotation = intialRoation;
    }

    public override void InitializeInteractable()
    {
        //intialRoation = hebel.transform.localRotation;
    }

    public override void UpdateInteractable()
    {
    }
}
