using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hebel : Interactable
{
    public bool inOnPosition = false;
    [SerializeField] private GameObject hebel;
    private Quaternion intialRoation;

    public override void ExecuteInteractionAnimation()
    {
        if (inOnPosition)
        {
            Debug.Log("## Hebel On");
            //hebel.transform.Rotate(0,45, 0, Space.Self);
            hebel.transform.localRotation = intialRoation;
            inOnPosition = false;
        }
        else
        {
            Debug.Log("## Hebel Off");
            // hebel.transform.Rotate(0,-45, 0, Space.Self);
            hebel.transform.rotation = intialRoation*Quaternion.Euler(0,45,0);
            inOnPosition = true;
        }
    }

    public override void HandleInteractableReset()
    {
        hebel.transform.localRotation = intialRoation;
    }

    public override void InitializeInteractable()
    {
        intialRoation = hebel.transform.localRotation;
    }

    public override void UpdateInteractable()
    {
    }
}
