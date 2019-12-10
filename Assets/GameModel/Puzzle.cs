using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PuzzleType
{
    OpenCrypt, // Transport one item(key) to another(lock)
    RingBell, // Interactions between items(take bell hammer an move it)
    TurnOnLight, // Interact with an item to activate it
}

public class Puzzle
{
    public PuzzleType type;

    public string name = "Test";

    public string description;

    public bool solved;

    public List<PuzzlePart> parts = new List<PuzzlePart>();

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        solved = parts.TrueForAll(part => part.IsSolved());
        if (solved)
        {
            Debug.Log("Quest:" + name + " was solved");
        }
    }

    public bool IsSolved()
    {
        return solved;
    }

}
