using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TouchItemRangeWatcher : MonoBehaviour
{
    private Dictionary<int, Item> nearbyItems = new Dictionary<int, Item>();
    [SerializeField] bool highlightNearbyItems = true;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public bool IsItemNearby(Item i)
    {
        if (i == null)
            return false;

        return nearbyItems.ContainsKey(i.GetInstanceID());
    }

    public void OnTriggerEnter(Collider col)
    {
        Item interactable = col.gameObject.GetComponent<Item>();
        if (interactable != null)
        {
            //Debug.Log("ItemWatcher Trigger Enter");
            if (!nearbyItems.ContainsKey(interactable.GetInstanceID()))
                nearbyItems.Add(interactable.GetInstanceID(), interactable);
            if (highlightNearbyItems)
                OutlineItem(col, true);
        }
    }

    public void OnTriggerStay(Collider col)
    {
    }

    public void OnTriggerExit(Collider col)
    {
        Item interactable = col.gameObject.GetComponent<Item>();
        if (interactable != null)
        {
            //Debug.Log("ItemWatcher Trigger Exit");
            nearbyItems.Remove(interactable.GetInstanceID());
            if (highlightNearbyItems)
                OutlineItem(col, false);
        }
    }

    private static void OutlineItem(Collider col, bool enabled)
    {
        Outline outline = col.gameObject.GetComponent<Outline>();
        if (outline != null)
        {
            outline.enabled = enabled;
        }
    }

}
