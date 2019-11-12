using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour, PuzzlePart
{
    private enum ItemType
    {
        CryptKey, // Used to unlock the crypt
    }

    [SerializeField] private ItemType m_itemType;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Collision Enter: " + gameObject.name);
    }

    private void OnCollisionStay(Collision collision)
    {
    }

    private void OnCollisionExit(Collision collision)
    {
        Debug.Log("Collision Exit: " + gameObject.name);
    }


    public bool isSolved()
    {
        return false;
    }

    public void trigger()
    {

    }
}
