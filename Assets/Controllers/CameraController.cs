using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    //[SerializeField] public bool Debug_touchModeEnabled = false;
    [SerializeField] public DecoupledInputManager inputManager;
    LayerMask mask;
    // Start is called before the first frame update
    void Start()
    {
        mask = LayerMask.GetMask("Item");

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
        Debug.Log("CollectItemFromTouchRaycast()");
        for (int i = 0; i < Input.touchCount; ++i)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);
            CollectOnRaycastHit(ray);
        }
    }

    private void CollectOnRaycastHit(Ray ray)
    {
        RaycastHit hit;
        int layer_mask = LayerMask.GetMask("Player","Collector");
        int layer_mask_inv = ~layer_mask;
        float distance = Mathf.Infinity;
        bool trigger = Physics.Raycast(ray, out hit, distance, layer_mask_inv);
            Debug.Log("hit: "+ trigger + " - " + hit.collider?.name);
        GameObject.FindObjectOfType<CharacterController>().CollectOrDropFromRaycast(trigger, hit);
    }
}