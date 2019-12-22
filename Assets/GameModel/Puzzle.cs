using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Puzzles
{
    OpenCrypt, // Transport one item(key) to another(lock)
    RingBell, // Interactions between items(take bell hammer an move it)
    TurnOnLight, // Interact with an item to activate it
}

public class Puzzle: MonoBehaviour
{
    [SerializeField]
    private Puzzles type;

    [SerializeField]
    private string displayName;

    [SerializeField]
    private string description;

    private List<PuzzlePart> parts;

    private bool solved;

    void Start()
    {
        parts = new List<PuzzlePart>(GetComponentsInChildren<PuzzlePart>());
        Debug.Log("Found parts: "+parts.Count);
    }

    void Update()
    {
        solved = parts.TrueForAll(part => part.IsSolved());
        if (solved)
        {
            //Debug.Log("Quest:" + displayName + " was solved");
        }
    }

    public bool IsSolved()
    {
        return solved;
    }

    public string GetDisplayName()
    {
        return this.displayName;
    }

}
