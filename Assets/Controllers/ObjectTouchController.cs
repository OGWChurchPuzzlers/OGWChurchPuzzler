﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;

[System.Serializable] public class _UnityEventRayCastHit : UnityEvent<bool, RaycastHit> { }

/// <summary>
/// Immer wenn User auf Bildschirm klickt wird per Raycast das angeklickte objekt bestimmt.
/// </summary>
public class ObjectTouchController : MonoBehaviour
{
    //[SerializeField] public bool Debug_touchModeEnabled = false;
    [SerializeField] public DecoupledInputManager inputManager;
    [SerializeField] string[] ignoredLayers;

    [Header("Is invoked when user Touches an object")]
    public _UnityEventRayCastHit ObjectTouched;

    public ObjectTouchController()
    {
        ignoredLayers = new string[] { "Player", "Collector" };
    }
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        switch (inputManager.Mode)
        {
            case GameInputMode.Keyboard:
                CollectItemFromMouseRaycast();
                break;
            case GameInputMode.Touch_1:
                CollectItemFromTouchRaycast();
                break;
            default:
                break;
        }
    }

    private void CollectItemFromMouseRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            // Debug.Log("CollectItemFromMouseRaycast()");
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            CollectOnRaycastHit(ray);
        }
    }


    private void CollectItemFromTouchRaycast()
    {
        //Debug.Log("CollectItemFromTouchRaycast()");
        for (int i = 0; i < Input.touchCount; ++i)
        {
            var touch = Input.GetTouch(i);
            if (touch.phase == TouchPhase.Began)
            {
                Ray ray = Camera.main.ScreenPointToRay(touch.position);
                CollectOnRaycastHit(ray);
            }
            //else
            //{
            //    Debug.Log("CollectItemFromTouchRaycast cancel: " + touch.phase);
            //}
        }
    }

    private void CollectOnRaycastHit(Ray ray)
    {
        if (!EventSystem.current.IsPointerOverGameObject()) // verhindere klick durch UI
        {
            RaycastHit hit;

            int layer_mask_inv = ~LayerMask.GetMask(ignoredLayers);
            float distance = Mathf.Infinity;
            bool trigger = Physics.Raycast(ray, out hit, distance, layer_mask_inv);

            //Debug.Log("hit: " + trigger + " - " + hit.collider?.name);
            ObjectTouched.Invoke(trigger, hit);
        }
    }
}