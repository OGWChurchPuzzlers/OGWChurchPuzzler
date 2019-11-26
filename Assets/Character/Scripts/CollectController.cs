using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Debug.Log("Collect Trigger Enter");
        characterController.SetCollectableItem(col.gameObject);
        var outline = col.gameObject.GetComponent<Outline>();
        if(outline != null)
        {
            outline.OutlineWidth = 5f;
        }

    }
    private void OnTriggerStay(Collider col)
    {
    }

    public void OnTriggerExit(Collider col)
    {
        Debug.Log("Collect Trigger Exit");
        var outline = col.gameObject.GetComponent<Outline>();
        if (outline != null)
        {
            outline.OutlineWidth = 0f;
        }
        characterController.SetCollectableItem(null);
    }


}
