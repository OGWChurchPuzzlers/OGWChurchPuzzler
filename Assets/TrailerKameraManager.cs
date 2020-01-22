using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailerKameraManager : MonoBehaviour
{

    public Camera main;
    public Camera cam1;
    public Camera cam2;
    public Camera cam3;
    public Camera cam4;
    public Camera cam5;
    public Camera cam6;
    public Camera cam7;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            disableAllCams();
            main.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            disableAllCams();

            cam1.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            disableAllCams();

            cam2.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            disableAllCams();

            cam3.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            disableAllCams();

            cam4.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            disableAllCams();

            cam5.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            disableAllCams();

            cam6.enabled = true;
        }
        if (Input.GetKeyDown(KeyCode.Alpha7))
        {
            disableAllCams();

            cam7.enabled = true;
        }

    }

    public void disableAllCams()
    {
        main.enabled = false;
        cam1.enabled = false;
        cam2.enabled = false;
        cam3.enabled = false;
        cam4.enabled = false;
        cam5.enabled = false;
        cam6.enabled = false;
        cam7.enabled = false;
    }
}
