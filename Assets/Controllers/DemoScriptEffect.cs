using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DemoScriptEffect : MonoBehaviour, ScriptEffect
{
    int iteration = 0;

    public void ExecuteScriptedEffect(string arg)
    {
        string[] c = arg.Split(',');
        Color colorToSet = Color.cyan;
        int indexToShow = iteration % c.Length;
        if (c.Length > 0)
        {
            switch (c[indexToShow])
            {
                case "red":
                    colorToSet = Color.red;
                    break;
                case "green":
                    colorToSet = Color.green;
                    break;
                case "blue":
                    colorToSet = Color.blue;
                    break;
                case "yellow":
                    colorToSet = Color.yellow;
                    break;
                case "black":
                    colorToSet = Color.black;
                    break;
                default:
                    break;
            }
        }
        gameObject.GetComponent<Renderer>().material.color = colorToSet;
        iteration++;
    }

    // Start is called before the first frame update
    void Start()
    {
        iteration = 0;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
