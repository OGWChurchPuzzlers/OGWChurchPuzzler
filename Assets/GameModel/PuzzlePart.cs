using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PuzzleCategorie
{
    Physical, // Item needs to have physical contact with the puzzle
    Posession, // Item needs to be carried/possesed when near the puzzle
    Interaction, // Item needs to be activated by the user directly (press E near it)
}

public class PuzzlePart : MonoBehaviour
{
    private const KeyCode INTERACTION_KEY = KeyCode.E;

    [SerializeField] private string description;

    [SerializeField] private Item trigger;

    [SerializeField] private PuzzleCategorie categorie;

    private bool isSolved = false;

    private bool isInteractionTriggered = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        handleInteractionTriggerPressed();
    }

    private void handleInteractionTriggerPressed()
    {
        if (Input.GetKeyUp(INTERACTION_KEY))
        {
            this.isInteractionTriggered = true;
        }
        else
        {
            this.isInteractionTriggered = false;
        }
    }


    /**
     *  The puzzle part has a physical trigger with which it can react to contact with other objects
     */
    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("Object enters puzzle test.");
        switch (this.categorie)
        {
            case PuzzleCategorie.Physical:
                HandlePhyiscalTrigger(col);
                break;
            case PuzzleCategorie.Posession:
                HandlePosessionTrigger();
                break;
            default:
                //Debug.Log("Cannot trigger effect for unknown puzzle categorie");
                break;
        }
    }

    private void OnTriggerStay(Collider col)
    {
        //Debug.Log("Object enters puzzle test.");
        switch (this.categorie)
        {
            case PuzzleCategorie.Interaction:
                HandleInteractionTrigger(col);
                break;
            default:
                //Debug.Log("Cannot trigger effect for unknown puzzle categorie");
                break;
        }
    }

    private void OnTriggerExit(Collider col)
    {
        //Debug.Log("Object leaves puzzle test.");
    }

    private void HandlePhyiscalTrigger(Collider col)
    {
        if (IsColliderTrigger(col)) // Test if the puzzlepart collided with its trigger (f.e. a block is placed on its counter part)
        {
            Debug.Log("Object solved the puzzle part.");
            this.isSolved = true;
            Despawn(gameObject);
        }
    }

    private void HandlePosessionTrigger()
    {
        if (IsTriggerInPossesion()) // Test if the character is carrying the trigger (f.e. the key is carried when near the crypt door)
        {
            Debug.Log("Object solved the puzzle part.");
            this.isSolved = true;
            Despawn(gameObject);
        }
    }

    private void HandleInteractionTrigger(Collider col)
    {
        if (IsTriggerInteractedWith(col)) // Test if the character is carrying the trigger (f.e. the key is carried when near the crypt door)
        {
            Debug.Log("Object solved the puzzle part.");
            this.isSolved = true;
            Despawn(gameObject);
        }
    }

    private bool IsTriggerInteractedWith(Collider col)
    {
        return IsColliderTrigger(col) && this.isInteractionTriggered;
    }

    private bool IsColliderTrigger(Collider col)
    {
        Item item = col.gameObject.GetComponent<Item>();
        return item.gameObject.Equals(trigger.gameObject);
    }

    private bool IsTriggerInPossesion()
    {
        CharacterController characterController = GameObject.FindObjectOfType<CharacterController>();
        Debug.Log(characterController);
        // Is the currently collected item the trigger?
        return characterController.GetCollectedItem().gameObject.Equals(trigger.gameObject);
    }

    public bool IsSolved()
    {
        return isSolved;
    }


    // TODO: What should happen after trigger is succesful?
    private void Despawn(GameObject gameObject)
    {
        Destroy(gameObject);
    }
}
