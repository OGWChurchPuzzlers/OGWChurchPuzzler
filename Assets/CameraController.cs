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
            // ## Touch Mode

            for (int i = 0; i < Input.touchCount; ++i)
            {
                if (Input.GetTouch(i).phase == TouchPhase.Began)
                {
                    // Construct a ray from the current touch coordinates
                    Ray ray = Camera.main.ScreenPointToRay(Input.GetTouch(i).position);

                    // Create a particle if hit
                    RaycastHit hit;
                    if (Physics.Raycast(ray, out hit))
                    {
                        //                    Instantiate(particle, transform.position, transform.rotation);
                        // the object identified by hit.transform was clicked
                        // do whatever you want
                        Debug.Log("#### object touched + item hit");
                        Debug.Log(hit.transform.gameObject);
                        GameObject.FindObjectOfType<CharacterController>().AttachObject();
                    }
                }
            }
        }
        else
        {
            // ## Keyboard Mode
            if (Input.GetMouseButtonDown(0))
            { // if left button pressed...
                Ray ray = GetComponent<Camera>().ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit))
                {
                    // the object identified by hit.transform was clicked
                    // do whatever you want
                    Debug.Log(hit.transform.gameObject);
                    GameObject.FindObjectOfType<CharacterController>().AttachObject();
                }
            }
        }
    }
}
