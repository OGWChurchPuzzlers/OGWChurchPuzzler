using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIPuzzleRow : MonoBehaviour
{
    [SerializeField] Text header;

    [SerializeField] Text part_01;
    [SerializeField] Text part_02;
    [SerializeField] Text part_03;
    [SerializeField] Text part_04;


    public void updateRow(Puzzle a_p)
    {
        header.text = a_p.name;

        foreach (var item in a_p.GetParts())
        {

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
