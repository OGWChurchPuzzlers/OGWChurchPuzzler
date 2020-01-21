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

    /*
     * TODO:
     * - Wie legen wir fest welches puzzle gerade aktiv ist?
     * - Wie legen wir fest welches als nächstes kommt?
     */

    void Start()
    {
        //puzzleLabel = GameObject.FindGameObjectWithTag("PuzzleName").GetComponent<Text>();
        //descriptionLabels = new List<GameObject>(GameObject.FindGameObjectsWithTag("PuzzlePartDescription"));

        itemLabel = GameObject.FindGameObjectWithTag("ItemName").GetComponent<Text>();

        characterController = GameObject.FindObjectOfType<CharacterController>();

        this.puzzles = new List<Puzzle>(GameObject.FindObjectsOfType<Puzzle>());
        SetActivePuzzle();
    }

    private bool hasDescriptionSizeBeenChecked = false;
    void Update()
    {
        if (!hasDescriptionSizeBeenChecked)
        {
            int cnt_puzzleparts = this.activePuzzle.GetParts().Count;
            //if (descriptionLabels.Count < cnt_puzzleparts)
            //    Debug.LogError("There are not enough labels to show all parts of current Puzzle (" + cnt_puzzleparts + "/" + descriptionLabels.Count + ")");
            //hasDescriptionSizeBeenChecked = true;
        }
        //UpdatePuzzleUI();

        UpdateItemUI();
    }


    private void SetActivePuzzle()
    {
        this.activePuzzle = this.puzzles[1];
        Debug.Log("Set active puzzle to: " + activePuzzle.GetDisplayName());

    }
    private void UpdatePuzzleUI()
    {
        var solvedPuzzles = GetSolvedPuzzles();
        puzzleLabel.text = activePuzzle.GetDisplayName();
        if (activePuzzle != null && activePuzzle.IsSolved())
        {
            puzzleLabel.color = new Color(47 / 255f, 145 / 255f, 22 / 255f);
        }

        List<PuzzlePart> parts = activePuzzle.GetParts();
        for (int index = 0; index < parts.Count; index++)
        {
            var part = parts[index];
            string label = part.GetDescription();
            if (part.IsSolved())
            {
                label += check;
            }
            if(index < descriptionLabels.Count)
                descriptionLabels[index].GetComponent<Text>().text = label;
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
