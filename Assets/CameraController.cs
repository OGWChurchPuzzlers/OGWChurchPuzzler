using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] public bool Debug_touchModeEnabled = false;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (Debug_touchModeEnabled)
        {
            CollectItemFromTouchRaycast();
        }
        else
        {
            CollectItemFromMouseRaycast();
        }
    }

    private void CollectItemFromMouseRaycast()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
            CollectOnRaycastHit(ray);
        }
    }


    private void CollectItemFromTouchRaycast()
    {
        for (int i = 0; i < Input.touchCount; ++i)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
            CollectOnRaycastHit(ray);
        }
    }

    private void CollectOnRaycastHit(Ray ray)
    {
        RaycastHit hit;
        bool trigger = Physics.Raycast(ray, out hit);
        GameObject.FindObjectOfType<CharacterController>().CollectOrDrop(trigger);
    }
}