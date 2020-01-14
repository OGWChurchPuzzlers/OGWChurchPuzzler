using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    //private CharacterController characterController;
    [SerializeField] private DecoupledInputManager inputManager;
    private List<Interactable> nearbyInteractables = new List<Interactable>();
    // Start is called before the first frame update
    void Start()
    {
        //characterController = transform.parent.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (inputManager.IsInteractionKeyPressed())
        {
            //Debug.Log("btn pressed");
            // triggere alle nearby Interactables
            foreach (var interactable in nearbyInteractables)
            {
                //Debug.Log("Interactable Triggering");
                interactable.TriggerInteractable();
            }
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        Interactable interactable = col.gameObject.GetComponent<Interactable>();
        if (interactable != null)
        {
            //Debug.Log("Interactable Trigger Enter");
            nearbyInteractables.Add(interactable);
            OutlineItem(col);
        }
    }

    private static void OutlineItem(Collider col)
    {
        Outline outline = col.gameObject.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = true;
        }
    }

    public void OnTriggerStay(Collider col)
    {
    }

    public void OnTriggerExit(Collider col)
    {
        Interactable interactable = col.gameObject.GetComponent<Interactable>();
        if (interactable != null)
        {
            //Debug.Log("Interactable Trigger Exit");
            nearbyInteractables.Remove(interactable);
            OutlineItem(col);
            var outline = col.gameObject.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
            }
        }
    }

}
