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
    bool isSolved();

    // Trigger something the puzzle part does -> f.e. turn on or change light
    void trigger();
}

public class Puzzle : MonoBehaviour
{
    private enum Puzzles
    {
        CryptKey, // Transport one item(key) to another(lock)
        RingBell, // Interactions between items(take bell hammer an move it)
        TurnOnLight, // Interact with an item to activate it
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO: test generic if a puzzle is solved -> solved if all parts are solved
    bool isSolved()
    {
        return false;
    }
}
