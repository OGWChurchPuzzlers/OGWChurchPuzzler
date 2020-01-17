using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    SacristyKey,
    Torch,
    Button,
    Lever
}

public enum ItemCarryLocation
{
    Hand,
    In_Front
}

public class Item : MonoBehaviour
{

    [SerializeField] private ItemType m_itemType;
    [SerializeField] private ItemCarryLocation m_carryLocation;
    [SerializeField] private Vector3 m_offset;

    [SerializeField] private string m_description;

    [SerializeField] private bool DebugCollisionsLogs = false; 

    private Quaternion originalRotation;
    public ItemCarryLocation GetCarryLocation()
    {
        return m_carryLocation;
    }

    public Vector3 GetCarryOffset()
    {
        return m_offset;
    }
    // Start is called before the first frame update
    void Start()
    {
        Outline outline = gameObject.GetComponent<Outline>();
        if(outline != null)
        {
            outline.enabled = false;
        }
        this.originalRotation = transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnCollisionEnter(Collision collision)
    {
        if(DebugCollisionsLogs)
            Debug.Log("Collision Enter: " + gameObject.name);
    }

    private void OnCollisionStay(Collision collision)
    {
        //if (DebugCollisionsLogs)
        //    Debug.Log("Collision Stay: " + gameObject.name);
    }

    private void OnCollisionExit(Collision collision)
    {
        if (DebugCollisionsLogs)
            Debug.Log("Collision Exit: " + gameObject.name);
    }

    public Quaternion GetOrignalRotation()
    {
        return this.originalRotation;
    }

    public string GetDescription()
    {
        return this.m_description;
    }
}
