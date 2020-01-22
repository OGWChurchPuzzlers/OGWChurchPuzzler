using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Puzzles
{
    OpenSacristy, // Transport one item(key) to another(lock)
    RingBell, // Interactions between items(take bell hammer an move it)
    TurnOnLight, // Interact with an item to activate it,
    BuildPlayground
}

public class Puzzle: MonoBehaviour
{
    [SerializeField]
    AudioClip solvedSound;

    [SerializeField]
    AudioSource audiosrc;

    bool hasSolvedSoundPlayed = false;

    [SerializeField]
    private Puzzles type;

    [SerializeField]
    private string displayName;

    [SerializeField]
    private string description;

    private List<PuzzlePart> parts;

    private bool solved;

    void Start()
    {
        parts = new List<PuzzlePart>(GetComponentsInChildren<PuzzlePart>());
        Debug.Log("Found parts: "+parts.Count);
    }

    void Update()
    {
        solved = parts.TrueForAll(part => part.IsSolved());
        if (solved && !hasSolvedSoundPlayed)
        {
            PlaySolvedSound();
            hasSolvedSoundPlayed = true;
            Debug.Log("Quest:" + displayName + " was solved");
        }
    }

    public void PlaySolvedSound()
    {
        if (audiosrc != null && solvedSound != null) {
            audiosrc.PlayOneShot(solvedSound);
        }
    }

    public bool IsSolved()
    {
        return solved;
    }

    public string GetDisplayName()
    {
        return this.displayName;
    }

    public List<PuzzlePart> GetParts()
    {
        return this.parts;
    }
}
