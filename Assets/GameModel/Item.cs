﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum ItemType
{
    CryptKey, // Used to unlock the crypt
}

public class Item : MonoBehaviour
{

    [SerializeField] private ItemType m_itemType;

    // Start is called before the first frame update
    void Start()
    {
        Outline outline = gameObject.GetComponent<Outline>();
        outline.enabled = false;
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
}