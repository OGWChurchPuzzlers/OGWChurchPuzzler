﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour, ScriptEffect
{

    public void ExecuteScriptedEffect(string str)
    {
        PlaySound(str);
    }

    public AudioClip tueroeffnenSound, applauseSound, organSound, glockeSound;
    AudioSource audiosrc;

    // Start is called before the first frame update
    void Start()
    {
        tueroeffnenSound = Resources.Load<AudioClip>("Tueroeffnen") as AudioClip;
        applauseSound = Resources.Load<AudioClip>("applause") as AudioClip;
        organSound = Resources.Load<AudioClip>("organ") as AudioClip;
        glockeSound = Resources.Load<AudioClip>("Glocke") as AudioClip;


        audiosrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlaySound (string clip)
    {
        Debug.Log("Play Sound Test");
        switch (clip)
        {
            case "tuer":
                Debug.Log("Test Tuer Sound");
                audiosrc.PlayOneShot(tueroeffnenSound);
                break;
            case "organ":
                Debug.Log("TestSoundGlocke");
                audiosrc.PlayOneShot(applauseSound);
                break;
            case "applause":
                audiosrc.PlayOneShot(organSound);
                break;
            case "glocke":
                audiosrc.PlayOneShot(glockeSound);
                break;
        }
    }
}