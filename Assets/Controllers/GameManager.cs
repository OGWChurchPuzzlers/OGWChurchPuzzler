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

    [SerializeField] private GameObject puzzle_ui_row_sakristei;
    [SerializeField] private GameObject puzzle_ui_row_Orgel;
    [SerializeField] private GameObject puzzle_ui_row_Spielplatz;
    [SerializeField] private GameObject puzzle_ui_row_Turm;

    [SerializeField] Puzzle puzzle_sakristei;
    [SerializeField] Puzzle puzzle_spielplatz;
    [SerializeField] Puzzle puzzle_turm;
    [SerializeField] Puzzle puzzle_orgel;

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
        //if (!hasDescriptionSizeBeenChecked)
        //{
        //    int cnt_puzzleparts = this.activePuzzle.GetParts().Count;
        //    //if (descriptionLabels.Count < cnt_puzzleparts)
        //    //    Debug.LogError("There are not enough labels to show all parts of current Puzzle (" + cnt_puzzleparts + "/" + descriptionLabels.Count + ")");
        //    //hasDescriptionSizeBeenChecked = true;
        //}
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
        // old
        //var solvedPuzzles = GetSolvedPuzzles();
        //puzzleLabel.text = activePuzzle.GetDisplayName();
        //if (activePuzzle != null && activePuzzle.IsSolved())
        //{
        //    puzzleLabel.color = new Color(47 / 255f, 145 / 255f, 22 / 255f);
        //}

        //List<PuzzlePart> parts = activePuzzle.GetParts();
        //for (int index = 0; index < parts.Count; index++)
        //{
        //    var part = parts[index];
        //    string label = part.GetDescription();
        //    if (part.IsSolved())
        //    {
        //        label += check;
        //    }
        //    if(index < descriptionLabels.Count)
        //        descriptionLabels[index].GetComponent<Text>().text = label;
        //}
        //var rows = GameObject.FindGameObjectWithTag("PuzzleName").GetComponent<Text>();

        
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


        fillRow(puzzle_ui_row_sakristei, puzzle_sakristei);

        void fillRow(GameObject a_ui_row, Puzzle a_p)
        {

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

public static class Helper
{
    public static T[] FindComponentsInChildrenWithTag<T>(this GameObject parent, string tag, bool forceActive = false) where T : Component
    {
        if (parent == null) { throw new System.ArgumentNullException(); }
        if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }
        List<T> list = new List<T>(parent.GetComponentsInChildren<T>(forceActive));
        if (list.Count == 0) { return null; }

        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].CompareTag(tag) == false)
            {
                list.RemoveAt(i);
            }
        }
        return list.ToArray();
    }

    public static T FindComponentInChildWithTag<T>(this GameObject parent, string tag, bool forceActive = false) where T : Component
    {
        if (parent == null) { throw new System.ArgumentNullException(); }
        if (string.IsNullOrEmpty(tag) == true) { throw new System.ArgumentNullException(); }

        T[] list = parent.GetComponentsInChildren<T>(forceActive);
        foreach (T t in list)
        {
            if (t.CompareTag(tag) == true)
            {
                return list[0];
            }
        }
        return null;
    }
}