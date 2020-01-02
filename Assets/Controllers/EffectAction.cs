using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PuzzlePartEffect
{
    Despawn, // an object will be despawned
    Spawn, // an object will be spawned, 
    SetActive, // an object and its children is activated
    SetInactive, // an object and its its children is deactivated
    ToggleActiveState,
    ExecuteScriptEffect,
    TriggerInteractable,
}

[Serializable]
public class EffectAction
{
    [SerializeField] public PuzzlePartEffect effect;
    [SerializeField] public GameObject arg;
    [SerializeField] public string arg2 = "";

}

public interface ScriptEffect
{
    void ExecuteScriptedEffect(String arg);
}