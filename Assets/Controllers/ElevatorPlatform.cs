using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Verhindert "Bounce","Stottern" der Spielfigur wenn sich der Lift bewegt,
/// indem es den Spieler bei Bewegung mitbewegt.
/// Funktioniert am besten, wenn Trigger etwas höher ist als eigentlicher Collider -> dann auch beim herunterfahren keine Probleme
/// </summary>
public class ElevatorPlatform : MonoBehaviour
{
    private List<GameObject> objectsOnPlatform = new List<GameObject>();
    private Vector3 lastPosistion;
    public Vector3 DBG_LastMoveDirection;

    // Start is called before the first frame update
    void Start()
    {
        lastPosistion = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 posChange = gameObject.transform.position - lastPosistion;
        DBG_LastMoveDirection = posChange;

        lastPosistion = gameObject.transform.position;
        if (lastPosistion != Vector3.zero)
        {
            foreach (var g in objectsOnPlatform)
            {
                //Debug.Log("##Elevator Move");
                g.transform.Translate(posChange);
            }
        }

    }

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("##Elevator OnTriggerEnter");
        objectsOnPlatform.Add(col.gameObject);

    }

    private void OnTriggerStay(Collider col)
    {
        //Debug.Log("##BTN OnTriggerStay");


    }

    private void OnTriggerExit(Collider col)
    {
        //Debug.Log("##Elevator OnTriggerLeave");
        objectsOnPlatform.Remove(col.gameObject);
    }
}
