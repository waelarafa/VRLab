using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SpectoAudioManager : MonoBehaviour
{
    
    public static SpectoAudioManager Instance { get; set; }
    
    [SerializeField] private AudioClip spectoExperience_Step1_Audio;
    [SerializeField] private AudioClip spectoExperience_Step2_Audio;
    [SerializeField] private AudioClip spectoExperience_Step3_Audio;
    [SerializeField] private AudioClip spectoExperience_Step4_Audio;
    [SerializeField] private AudioClip spectoExperience_Step5_Audio;
    [SerializeField] private AudioClip spectoExperience_Step6_Audio;
    [SerializeField] private AudioClip spectoExperience_Step7_Audio;
    [SerializeField] private AudioClip spectoExperience_Step8_9_Audio;
    [SerializeField] private AudioClip spectoExperience_Step8_Audio;
    [SerializeField] private AudioClip spectoExperience_Step9_Audio;
    [SerializeField] private AudioClip spectoExperience_Step10_Audio;
    [SerializeField] private AudioClip spectoExperience_Step11_Audio;
    
    private AudioSource audioSource;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        SpectoLabManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
        
        PlayAudio(spectoExperience_Step1_Audio);
    }
    
    
    private void Update()
    {
        if (SpectoLabManager.Instance.ExperienceState == SpectoExperienceState.AddingAscorbicAcid)
        {
            if (!audioSource.isPlaying)
            {
                SpectoUIManager.Instance.InitialTextGameObject.SetActive(false);
            }
        }
    }
    
    
    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {

        switch (SpectoLabManager.Instance.ExperienceState)
        {
            case SpectoExperienceState.AddingAscorbicAcid:
                PlayAudio(spectoExperience_Step2_Audio);
                break;
            case SpectoExperienceState.AddingCombinedReagent:
                PlayAudio(spectoExperience_Step3_Audio);
                break;
            case SpectoExperienceState.PouringTheSolution:
                PlayAudio(spectoExperience_Step4_Audio);
                break;
            case SpectoExperienceState.MovingTheSolutionToTheMixer:
                PlayAudio(spectoExperience_Step5_Audio);
                break;
            case SpectoExperienceState.TurningOnTheMixer:
                break;
            case SpectoExperienceState.AgitatingTheSolution:
                break;
            case SpectoExperienceState.MovingTheSolutionToTheRack:
                PlayAudio(spectoExperience_Step6_Audio);
                break;
            case SpectoExperienceState.RestingTheSolution:
                PlayAudio(spectoExperience_Step7_Audio);
                break;
            case SpectoExperienceState.SettingTheWavelength:
                PlayAudio(spectoExperience_Step8_Audio);
                break;
            case SpectoExperienceState.FillingTheTube:
                PlayAudio(spectoExperience_Step9_Audio);
                break;
            case SpectoExperienceState.OpeningTheSpectoPhotometer:
                PlayAudio(spectoExperience_Step8_9_Audio);
                break;
            case SpectoExperienceState.MovingTheTube:
                PlayAudio(spectoExperience_Step10_Audio);
                break;
            case SpectoExperienceState.ShowcasingResults:
                PlayAudio(spectoExperience_Step11_Audio);
                break;
            case SpectoExperienceState.ClosingTheSpectoPhotometer:
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
