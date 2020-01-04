using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public enum InteractionTrigger
{
    None,
    OnPresence,
    OnKeyPress,
}


[Serializable]
public abstract class Interactable : MonoBehaviour
{
    [SerializeField] InteractionTrigger triggerType = InteractionTrigger.OnKeyPress;
    [Tooltip("Only Objects with this tag can trigger this Interactable")]
    [SerializeField] private String triggerTag = "Player";
    [SerializeField] bool triggerable = true;
    bool hasBeenTriggeredAtLeastOnce = false;

    [Tooltip("Object Stays in World but is not interactable anymore")]
    [SerializeField] bool onlyTriggerableOnce = false;
    [Tooltip("Use deactivate with care!")]
    [SerializeField] bool deactivateObjectAfterTrigger = false;

    [Tooltip("0 to execute no effects, one or more to execute actions e.g enable object x")]
    [SerializeField] private EffectAction[] effects = new EffectAction[0];
    private const KeyCode INTERACTION_KEY = KeyCode.E;
    private bool isInteractionKeyCurrentlyTriggered = false;
    private bool currentlyInRange = false;

    // Start is called before the first frame update
    void Start()
    {
        InitializeInteractable();
    }
    /// <summary>
    /// called on MonoBehaviour.Start()
    /// </summary>
    public abstract void InitializeInteractable();
    /// <summary>
    /// called on MonoBehaviour.Update()
    /// </summary>
    public abstract void UpdateInteractable();

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(INTERACTION_KEY))
        {
            this.isInteractionKeyCurrentlyTriggered = true;

            // Trigger in update behandeln, da Update und Triggerloop nicht in sync -> Key-Event könnte sonst übersehen werden
            UpdateTriggerState();
        }
        else
        {
            this.isInteractionKeyCurrentlyTriggered = false;
        }
        UpdateInteractable();
    }

    private void OnTriggerEnter(Collider col)
    {
        //Debug.Log("##BTN OnTriggerEnter");
        if (col.gameObject.tag == triggerTag)
            currentlyInRange = true;

        if (currentlyInRange && triggerType == InteractionTrigger.OnPresence)
        {
            Debug.Log("##INTERACTABLE: PresenceTrigger");
            TriggerInteractable();
        }

    }

    private void OnTriggerStay(Collider col)
    {
        //Debug.Log("##BTN OnTriggerStay");
    }

    private void OnTriggerExit(Collider col)
    {
        //Debug.Log("##BTN OnTriggerExit");
        if (col.gameObject.tag == triggerTag)
            currentlyInRange = false;
    }

    private void UpdateTriggerState()
    {
        switch (triggerType)
        {
            case InteractionTrigger.None:
                break;
            case InteractionTrigger.OnPresence:
                // already handled in OnTriggerEnter()
                break;
            case InteractionTrigger.OnKeyPress:
                if (currentlyInRange && isInteractionKeyCurrentlyTriggered)
                {
                    Debug.Log("##INTERACTABLE: KeyTrigger");
                    TriggerInteractable();
                }
                break;
            default:
                break;
        }
    }
    /// <summary>
    /// Manually Trigger Interactable. Be careful when you call it
    /// </summary>
    public void TriggerInteractable()
    {
        HandleInteraction();
    }

    public void ResetInteractable()
    {
        hasBeenTriggeredAtLeastOnce = false;
        HandleInteractableReset();
    }

    public abstract void HandleInteractableReset();

    // ## Interaction
    public void HandleInteraction()
    {
        Debug.Log("##INTERACTABLE: HandleInteraction");
        if (triggerable)
        {
            if (onlyTriggerableOnce && hasBeenTriggeredAtLeastOnce)
                return;
            hasBeenTriggeredAtLeastOnce = true;


            ExecuteInteractionAnimation();
            ExecuteEffects();

            if (deactivateObjectAfterTrigger)
                Despawn(gameObject);
        }
    }


    public abstract void ExecuteInteractionAnimation();

    private void ExecuteEffects()
    {
        Debug.Log("##INTERACTABLE: ExecuteEffects");

        foreach (var e in effects)
        {
            switch (e.effect)
            {
                case PuzzlePartEffect.Despawn:
                    if (e.arg != null)
                        Despawn(e.arg);
                    break;
                case PuzzlePartEffect.Spawn:
                    //Invoke("SpawnNext", 0.01f);
                    if (e.arg != null)
                        Spawn(e.arg);
                    break;
                case PuzzlePartEffect.SetActive:
                    if (e.arg != null)
                        e.arg.SetActive(true);
                    break;
                case PuzzlePartEffect.SetInactive:
                    if (e.arg != null)
                        e.arg.SetActive(false);
                    break;
                case PuzzlePartEffect.ToggleActiveState:
                    if (e.arg != null)
                        e.arg.SetActive(!e.arg.activeSelf);
                    break;
                case PuzzlePartEffect.ExecuteScriptEffect:
                    if (e.arg != null)
                    {
                        e.arg.GetComponent<ScriptEffect>()?.ExecuteScriptedEffect(e.arg2);
                    }
                    break;
                case PuzzlePartEffect.TriggerInteractable:
                    if (e.arg != null && e.arg != gameObject)
                        e.arg.GetComponent<Interactable>()?.TriggerInteractable();
                    break;
                default:
                    break;
            }
        }
    }

    private void Despawn(GameObject a_gameObject)
    {
        Destroy(a_gameObject);
    }

    private void Spawn(GameObject a_gameObject)
    {
        GameObject newObject = Instantiate(a_gameObject, transform.position, transform.rotation);
        //newObject.name = "123 Kiste";
        //Debug.Log("parent Object at: " + transform.position);
        //Debug.Log("Spawned Object at: " + newObject.transform.position);
    }
}

