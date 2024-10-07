using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    
    public static AudioManager Instance { get; set; }
    
    [Header("Phenolphthalein Alkalnity Audio Clips")] [SerializeField]
    private AudioClip experience1_Step1_Audio;

    [SerializeField] private AudioClip experience1_Step2_Audio;
    [SerializeField] private AudioClip experience1_Step3_Audio;
    [SerializeField] private AudioClip experience1_Step4_Audio;
    [SerializeField] private AudioClip experience1_Step5_Audio;
    [SerializeField] private AudioClip experience1_Step6_Audio;
    [SerializeField] private AudioClip experience1_Step7_Audio;

    [Header("Complete Phenolphthalein Alkalnity Audio Clips")] [SerializeField]
    private AudioClip experience2_Step1_Audio;

    [SerializeField] private AudioClip experience2_Step2_Audio;
    [SerializeField] private AudioClip experience2_Step3_Audio;
    [SerializeField] private AudioClip experience2_Step4_Audio;
    [SerializeField] private AudioClip experience2_Step5_Audio;
    [SerializeField] private AudioClip experience2_Step6_Audio;
    [SerializeField] private AudioClip experience2_Step7_Audio;


    private AudioSource audioSource;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        
        audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        LabManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;

        PlayStartAudio();
    }


    private void PlayStartAudio()
    {
        switch (LabManager.Instance.AlkalinityType)
        {
            case AlkalinityType.AlkalimetricTitration:
                PlayAudio(experience1_Step1_Audio);

                break;
            case AlkalinityType.CompleteAlkalimetricTitration:
                PlayAudio(experience2_Step1_Audio);

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        switch (LabManager.Instance.AlkalinityType)
        {
            case AlkalinityType.AlkalimetricTitration:

                switch (LabManager.Instance.ExperienceState)
                {
                    case ExperienceState.ChargingDropperWithIndicator:
                        PlayAudio(experience1_Step1_Audio);
                        break;
                    case ExperienceState.AddingIndicator:
                        break;
                    case ExperienceState.PouringWater:
                        PlayAudio(experience1_Step2_Audio);
                        break;
                    case ExperienceState.AddingCHl:
                        PlayAudio(experience1_Step3_Audio);
                        break;
                    case ExperienceState.AddingMagnet:
                        PlayAudio(experience1_Step4_Audio);
                        break;
                    case ExperienceState.MovingTheSolution:
                        PlayAudio(experience1_Step5_Audio);
                        break;
                    case ExperienceState.ActivatingTheStirrer:
                        break;
                    case ExperienceState.AddingCHlDrops:
                        PlayAudio(experience1_Step6_Audio);
                        break;
                    case ExperienceState.ShowcasingResults:
                        PlayAudio(experience1_Step7_Audio);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                break;
            case AlkalinityType.CompleteAlkalimetricTitration:

                switch (LabManager.Instance.ExperienceState)
                {
                    case ExperienceState.ChargingDropperWithIndicator:
                        break;
                    case ExperienceState.AddingIndicator:
                        PlayAudio(experience2_Step1_Audio);
                        break;
                    case ExperienceState.PouringWater:
                        PlayAudio(experience2_Step2_Audio);
                        break;
                    case ExperienceState.AddingCHl:
                        PlayAudio(experience2_Step3_Audio);
                        break;
                    case ExperienceState.AddingMagnet:
                        PlayAudio(experience2_Step4_Audio);
                        break;
                    case ExperienceState.MovingTheSolution:
                        PlayAudio(experience2_Step5_Audio);
                        break;
                    case ExperienceState.ActivatingTheStirrer:
                        break;
                    case ExperienceState.AddingCHlDrops:
                        PlayAudio(experience2_Step6_Audio);
                        break;
                    case ExperienceState.ShowcasingResults:
                        PlayAudio(experience2_Step7_Audio);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
                
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