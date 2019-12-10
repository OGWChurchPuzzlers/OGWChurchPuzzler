using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzlePart : MonoBehaviour
{
    [SerializeField] private string description;

    [SerializeField] private GameObject trigger;

    private bool isSolved = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // Physical trigger -> Object is placed somewhere
    private void OnTriggerEnter(Collider col)
    {
        Debug.Log("Object enters puzzle test.");
        if (IsColliderTrigger(col)) // if the puzzlepart collided with its trigger (f.e. a block is placed on its counter part)
        {
            Debug.Log("Object solved the puzzle part.");
            isSolved = true;
            Despawn(gameObject);
        }
    }

    private void OnTriggerStay(Collider col)
    {
        if (IsColliderTrigger(col)) // test if the physical puzzle is still solved
        {
            //Debug.Log("PuzzlePart is fulfilled.");
        }
    }

    private void OnTriggerExit(Collider col)
    {
        Debug.Log("Object leaves puzzle test.");
    }
    public bool IsSolved()
    {
        return isSolved;
    }

    void Despawn(GameObject gameObject)
    {
        Destroy(gameObject);
    }

    private bool IsColliderTrigger(Collider col)
    {
        return col.gameObject.Equals(trigger);
    }
}
