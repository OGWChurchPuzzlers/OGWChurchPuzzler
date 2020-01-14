using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableController : MonoBehaviour
{
    //private CharacterController characterController;
    [SerializeField] private DecoupledInputManager inputManager;
    private Dictionary<int, Interactable> nearbyInteractables = new Dictionary<int, Interactable>();
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
                interactable.Value.TriggerInteractable();
            }
        }
    }

    public void InteractFromRaycast(bool eventTriggered, RaycastHit hit) 
    {
        if (!eventTriggered)
            return;

        Collider c = hit.collider;
        Interactable toInteract = c.gameObject.GetComponent<Interactable>();

        if (toInteract != null && nearbyInteractables.ContainsKey(toInteract.GetInstanceID()))
        {
            nearbyInteractables[toInteract.GetInstanceID()]?.TriggerInteractable();
        }
    }

    public void OnTriggerEnter(Collider col)
    {
        Interactable interactable = col.gameObject.GetComponent<Interactable>();
        if (interactable != null)
        {
            //Debug.Log("## Interactable Trigger Enter");
            if (!nearbyInteractables.ContainsKey(interactable.GetInstanceID()))
                nearbyInteractables.Add(interactable.GetInstanceID(), interactable);
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
            nearbyInteractables.Remove(interactable.GetInstanceID());
            OutlineItem(col);
            var outline = col.gameObject.GetComponent<Outline>();
            if (outline != null)
            {
                outline.enabled = false;
            }
        }
    }

}
