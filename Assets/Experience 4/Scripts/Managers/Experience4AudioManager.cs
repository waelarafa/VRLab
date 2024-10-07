using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience4AudioManager : MonoBehaviour
{
    public static Experience4AudioManager Instance { get; set; }

    [SerializeField] private AudioClip experience4_Step1_Audio;
    [SerializeField] private AudioClip experience4_Step2_Audio;
    [SerializeField] private AudioClip experience4_Step3_Audio;
    [SerializeField] private AudioClip experience4_Step4_Audio;
    [SerializeField] private AudioClip experience4_Step5_Audio;
    [SerializeField] private AudioClip experience4_Step6_Audio;
    [SerializeField] private AudioClip experience4_Step7_Audio;
    [SerializeField] private AudioClip experience4_Step8_Audio;
    [SerializeField] private AudioClip experience4_Step9_Audio;


    private AudioSource audioSource;


    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        Experience4Manager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;

        PlayAudio(experience4_Step1_Audio);
    }


    private void Update()
    {
        if (Experience4Manager.Instance.ExperienceState == Experience4State.AddingDiazotizationReagent)
        {
            if (!audioSource.isPlaying)
            {
                Experience4UIManager.Instance.InitialTextGameObject.SetActive(false);
            }
        }
    }


    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        switch (Experience4Manager.Instance.ExperienceState)
        {
            case Experience4State.AddingDiazotizationReagent:
                break;
            case Experience4State.PouringTheSolution:
                PlayAudio(experience4_Step2_Audio);
                break;
            case Experience4State.MovingTheSolutionToTheMixer:
                PlayAudio(experience4_Step3_Audio);
                break;
            case Experience4State.TurningOnTheMixer:
                break;
            case Experience4State.AgitatingTheSolution:
                break;
            case Experience4State.MovingTheSolutionToTheRack:
                PlayAudio(experience4_Step4_Audio);
                break;
            case Experience4State.RestingTheSolution:
                break;
            case Experience4State.SettingTheWavelength:
                PlayAudio(experience4_Step5_Audio);
                break;
            case Experience4State.FillingTheTube:
                PlayAudio(experience4_Step7_Audio);
                break;
            case Experience4State.OpeningTheSpectoPhotometer:
                PlayAudio(experience4_Step6_Audio);
                break;
            case Experience4State.MovingTheTube:
                PlayAudio(experience4_Step8_Audio);
                break;
            case Experience4State.ShowcasingResults:
                PlayAudio(experience4_Step9_Audio);
                break;
            case Experience4State.ClosingTheSpectoPhotometer:


                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    private void PlayAudio(AudioClip audioClip)
    {
        audioSource.clip = audioClip;
        audioSource.Play();
    }


    public void PauseAudio()
    {
        audioSource.Pause();
    }


    public void ResumeAudio()
    {
       audioSource.UnPause(); 
    }
    
    
    
    
}