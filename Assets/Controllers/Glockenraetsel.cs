using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Glockenraetsel : MonoBehaviour, ScriptEffect
{

    [SerializeField] Animator m_anmiatorGlocken;
    [SerializeField] string m_anim_transition_ring_bell_big = "";
    [SerializeField] string m_anim_transition_ring_bell_small = "";
    [SerializeField] PuzzlePart puzzlepart;

    public void ExecuteScriptedEffect(string arg)
    {
        Debug.Log("Glockenrätsel: ExecuteScriptedEffect" + arg);
        switch (arg)
        {
            case "rope_L":
                if (m_anmiatorGlocken != null)
                {
                    m_anmiatorGlocken.SetBool(m_anim_transition_ring_bell_small, !m_anmiatorGlocken.GetBool(m_anim_transition_ring_bell_small));
                }
                break;
            case "rope_R":
                if (m_anmiatorGlocken != null)
                {
                    m_anmiatorGlocken.SetBool(m_anim_transition_ring_bell_big, !m_anmiatorGlocken.GetBool(m_anim_transition_ring_bell_big));
                }
                break;
            default:
                break;
        }

        if (!puzzlepart.IsSolved() && m_anmiatorGlocken.GetBool(m_anim_transition_ring_bell_big) && m_anmiatorGlocken.GetBool(m_anim_transition_ring_bell_small))
            puzzlepart.SolvePuzzlePart();
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
