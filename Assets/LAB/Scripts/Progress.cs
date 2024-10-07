using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Progress : MonoBehaviour
{
    public static Progress Instance { get; set; }
    
    public float toAdd;
    public float[] progress;
    public TextMeshProUGUI[] text;
    public AudioSource audsrc;
    public AudioClip[] clip;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;
    }


    public void AddToProgress(int p)
    {
        progress[p] += toAdd;
        text[p].text = "Progress " + progress[p].ToString() + "%";
    }

    public void PlayAudio(int p)
    {
        audsrc.clip = clip[p];
        audsrc.Play();
    }
    
    
    public void PauseAudio()
    {
        audsrc.Pause();
    }


    public void ResumeAudio()
    {
        audsrc.UnPause(); 
    }
    
    
    
}
