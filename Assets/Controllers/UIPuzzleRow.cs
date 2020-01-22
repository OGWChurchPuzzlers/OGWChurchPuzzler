using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPuzzleRow : MonoBehaviour
{
    [SerializeField] Text header;

    [SerializeField] Text[] part_s; // besser wäre hier dynamische erstellung von textelementen
    private static char check = '\u2713';


    public void UpdateRow(Puzzle a_p)
    {
        ClearTexts();
        bool isPuzzleSolved = a_p.IsSolved();
        header.text = a_p.GetDisplayName();
        if (isPuzzleSolved)
        {
            header.text = a_p.GetDisplayName() + check;
            header.color = new Color(47 / 255f, 145 / 255f, 22 / 255f);
        }

        var parts = a_p.GetParts();
        var textlabels = part_s.Length;
        for (int i = 0; i < parts.Count; i++)
        {
            var part = parts[i];
            var partSolved = part.IsSolved();
            if (part_s.Length <= i)
                break;

            // setzte Texte
            string textToset = parts[i].GetDescription();
            if (partSolved)
            {
                part_s[i].color = new Color(47 / 255f, 145 / 255f, 22 / 255f);
                textToset += check;
            }

            part_s[i].text = textToset;
            part_s[i].gameObject.SetActive(true);
        }
    }

    private void ClearTexts()
    {
        header.text = "";
        header.color = Color.black;

        foreach (var t in part_s)
        {
            t.text = "";
            t.color = Color.black;

            t.gameObject.SetActive(false);
        }
    }


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }
}
