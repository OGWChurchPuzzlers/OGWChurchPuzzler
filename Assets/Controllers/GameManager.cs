using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Puzzle activePuzzle;

    private List<Puzzle> puzzles;

    private Text puzzleLabel;

    private Text itemLabel;

    private List<GameObject> descriptionLabels;

    private CharacterController characterController;

    private static char check = '\u2713';

    void Start()
    {
        itemLabel = GameObject.FindGameObjectWithTag("ItemName").GetComponent<Text>();

        characterController = GameObject.FindObjectOfType<CharacterController>();

        this.puzzles = new List<Puzzle>(GameObject.FindObjectsOfType<Puzzle>());
        SetActivePuzzle();
    }

    private bool hasDescriptionSizeBeenChecked = false;
    void Update()
    {
        UpdatePuzzleUI();

        UpdateItemUI();
    }


    private void SetActivePuzzle()
    {
        this.activePuzzle = this.puzzles[1];
        Debug.Log("Set active puzzle to: " + activePuzzle.GetDisplayName());

    }
    private void UpdatePuzzleUI()
    {
        GameObject[] rows = GameObject.FindGameObjectsWithTag("UI_Puzzle_Row");
        var rowcount = rows.Length;

        for (int i = 0; i < rows.Length; i++)
        {
            var row = rows[i];

            if (rowcount < puzzles.Count)
            {
                break;
            }
            var s = row.GetComponent<UIPuzzleRow>();
            s.UpdateRow(puzzles[i]);
        }
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