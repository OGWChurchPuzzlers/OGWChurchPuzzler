using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * This controller manages collecting items. This includes:
 * - Highlighting items which are available for pickup
 * - Telling the character controller which item is currently available for pickup
 */
public class CollectController : MonoBehaviour
{
    private CharacterController characterController;

    // Start is called before the first frame update
    void Start()
    {
        characterController = transform.parent.gameObject.GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter(Collider col)
    {
        Item item = col.gameObject.GetComponent<Item>();
        if (item != null)
        {
            Debug.Log("Collect Trigger Enter");
            characterController.SetCollectableItem(item);
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
        Item item = col.gameObject.GetComponent<Item>();
        characterController.SetCollectableItem(item);
    }

    public void OnTriggerExit(Collider col)
    {
        Debug.Log("Collect Trigger Exit");
        var outline = col.gameObject.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = false;
        }
        characterController.SetCollectableItem(null);

    }

}
