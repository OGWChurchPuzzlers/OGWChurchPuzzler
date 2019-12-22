using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public Puzzle activePuzzle;

    public List<Puzzle> puzzles;

    private Text puzzleLabel;

    private Text itemLabel;

    private List<GameObject> descriptionLabels;

    private CharacterController characterController;
    /*
     * TODO:
     * - Wie legen wir fest welches puzzle gerade aktiv ist?
     * - Wie legen wir fest welches als nächstes kommt?
     */

    void Start()
    {
        puzzleLabel = GameObject.FindGameObjectWithTag("PuzzleName").GetComponent<Text>();
        descriptionLabels = new List<GameObject>(GameObject.FindGameObjectsWithTag("PuzzlePartDescription"));

        itemLabel = GameObject.FindGameObjectWithTag("ItemName").GetComponent<Text>();

        characterController = GameObject.FindObjectOfType<CharacterController>();

        this.puzzles = new List<Puzzle>(GameObject.FindObjectsOfType<Puzzle>());
        SetActivePuzzle();
    }

    void Update()
    {
        UpdateUI();
    }


    void SetActivePuzzle()
    {
        this.activePuzzle = this.puzzles[0];
        Debug.Log("Set active puzzle to: " + activePuzzle.GetDisplayName());
    }

    void UpdateUI()
    {
        UpdatePuzzleUI();

        UpdateItemUI();
    }

    private void UpdatePuzzleUI()
    {
        puzzleLabel.text = activePuzzle.GetDisplayName();
        if (activePuzzle != null && activePuzzle.IsSolved())
        {
            puzzleLabel.color = new Color(47/255f, 145/255f, 22/255f);
        }

        List<PuzzlePart> parts = activePuzzle.GetParts();
        descriptionLabels[0].GetComponent<Text>().text = parts[0].GetDescription();

    }

    private void UpdateItemUI()
    {
        Item item = GameObject.FindObjectOfType<CharacterController>().GetCollectedItem();
        if (item != null)
        {
            string itemName = item.GetDescription();
            itemLabel.text = itemName;
        }
        else
        {
            itemLabel.text = "";

        }
    }

    public List<Puzzle> GetSolvedPuzzles()
    {
        return puzzles.FindAll(puzzle => puzzle.IsSolved());
    }
}
