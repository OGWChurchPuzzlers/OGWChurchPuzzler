using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//A Puzzle Part can be:
//    - An item in the hand of the player - Does player own the item?
//    - A position - is Player standing (roughly) on the right spot
//    - Do two parts have contact?
//    - Player is triggering something with his physical attributes(weight, speed)
public interface PuzzlePart
{
    bool IsSolved();

    // Trigger something the puzzle part does -> f.e. turn on or change light
    void Trigger();
}

public class Puzzle : MonoBehaviour, PuzzlePart
{
    private enum Puzzles
    {
        CryptKey, // Transport one item(key) to another(lock)
        RingBell, // Interactions between items(take bell hammer an move it)
        TurnOnLight, // Interact with an item to activate it
    }

    public string puzzleName = "Test";

    private string description;

    private PuzzlePart[] parts;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsSolved())
        {
            Trigger();
        }
    }

    public bool IsSolved()
    {
        return false;
    }

    public void Trigger()
    {
    }
}
