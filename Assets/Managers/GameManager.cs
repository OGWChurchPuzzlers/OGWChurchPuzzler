using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // The currently played puzzle
    public Puzzle activePuzzle;

    public List<Puzzle> puzzles;

    /*
     * TODO:
     * - Wie legen wir fest welches puzzle gerade aktiv ist?
     * - Wie legen wir fest welches als nächstes kommt?
     */

    // Start is called before the first frame update
    void Start()
    {
        CreatePuzzles();
        SetActivePuzzle();
    }

    // Update is called once per frame
    void Update()
    {
        UpdateUI();
    }

    void CreatePuzzles()
    {
        //TODO: build a list of all puzzles which should be displayed in UI and playable
    }

    void SetActivePuzzle()
    {
        Text puzzleLabel = GameObject.FindGameObjectWithTag("PuzzleName").GetComponent<Text>();
        Debug.Log("Set active puzzle to: " + activePuzzle.name);
        puzzleLabel.text += activePuzzle.name;
    }

    void UpdateUI()
    {
        if (activePuzzle != null && activePuzzle.IsSolved())
        {
            Text puzzleLabel = GameObject.FindGameObjectWithTag("PuzzleName").GetComponent<Text>();
            puzzleLabel.color = Color.green;
        }

        Text itemLabel = GameObject.FindGameObjectWithTag("ItemName").GetComponent<Text>();
        Item item = GameObject.FindObjectOfType<CharacterController>().GetCollectedItem();
        if(item != null)
        {
            string itemName = item.GetDescription();
            itemLabel.text = itemName;
        } else
        {
            itemLabel.text = "";

        }
    }

    public List<Puzzle> GetSolvedPuzzles()
    {
        return puzzles.FindAll(puzzle => puzzle.IsSolved());
    }
}
