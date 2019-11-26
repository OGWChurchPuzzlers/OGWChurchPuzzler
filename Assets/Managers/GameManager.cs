using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{    
    // The currently played puzzle
    private Puzzle activePuzzle = new Puzzle();

    // There is a set of Puzzles to complete
    private Puzzle[] puzzles;

    /*
     * TODO:
     * - Wie legen wir fest welches puzzle gerade aktiv ist?
     * - Wie legen wir fest welches als nächstes kommt?
     */

    // Start is called before the first frame update
    void Start()
    {
        Text puzzleLabel = GameObject.FindGameObjectWithTag("PuzzleLabel").GetComponent<Text>();
        Debug.Log(GameObject.FindGameObjectWithTag("PuzzleLabel"));
        puzzleLabel.text += activePuzzle.puzzleName;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
