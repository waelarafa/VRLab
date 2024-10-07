using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class AmmoniumExperienceAudioManager : MonoBehaviour
{
   
    public static AmmoniumExperienceAudioManager Instance { get; set; }
    
    
    [SerializeField] private AudioClip ammoniumExperience_Step0_Audio;
    [SerializeField] private AudioClip ammoniumExperience_Step1_Audio;
    [SerializeField] private AudioClip ammoniumExperience_Step2_Audio;
    [SerializeField] private AudioClip ammoniumExperience_Step3_Audio;
    [SerializeField] private AudioClip ammoniumExperience_Step4_Audio;
    [SerializeField] private AudioClip ammoniumExperience_Step5_Audio;
    [SerializeField] private AudioClip ammoniumExperience_Step5_6_Audio;
    [SerializeField] private AudioClip ammoniumExperience_Step6_Audio;
    [SerializeField] private AudioClip ammoniumExperience_Step7_Audio;
    [SerializeField] private AudioClip ammoniumExperience_Step8_Audio;
    [SerializeField] private AudioClip ammoniumExperience_Step9_Audio;
    [SerializeField] private AudioClip ammoniumExperience_Step10_Audio;
    

    private AudioSource audioSource;
    
    private void Awake()
    {
        if(Instance == null)
            Instance = this;
        
        audioSource = GetComponent<AudioSource>();
    }
    
    private void Start()
    {
        AmmoniumExperienceManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
        
        PlayAudio(ammoniumExperience_Step0_Audio);
    }
    
    
    private void Update()
    {
        if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.AddingAlkalineSolution)
        {
            if (!audioSource.isPlaying)
            {
                AmmoniumExperienceUIManager.Instance.InitialTextGameObject.SetActive(false);
            }
        }
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        switch (AmmoniumExperienceManager.Instance.ExperienceState)
        {
            case AmmoniumExperienceState.AddingAlkalineSolution:
                break;
            case AmmoniumExperienceState.AddingPhenolNitroprusside:
                PlayAudio(ammoniumExperience_Step1_Audio);
                break;
            case AmmoniumExperienceState.AddingTheMagneticBar:
                PlayAudio(ammoniumExperience_Step2_Audio);
                break;
            case AmmoniumExperienceState.MovingTheSolutionToTheStirrer:
                PlayAudio(ammoniumExperience_Step3_Audio);
                break;
            case AmmoniumExperienceState.TurningOnTheStirrer:
                break;
            case AmmoniumExperienceState.MixingTheSolution:
                break;
            case AmmoniumExperienceState.PouringTheSolutionIntoTheTube:
                PlayAudio(ammoniumExperience_Step4_Audio);
                break;
            case AmmoniumExperienceState.ClosingTheTube:
                break;
            case AmmoniumExperienceState.MovingTheSolutionToTheCardbox:
                PlayAudio(ammoniumExperience_Step5_Audio);
                break;
            case AmmoniumExperienceState.ClosingTheCardBox:
                break;
            case AmmoniumExperienceState.CoveringTheSolution:
                break;
            case AmmoniumExperienceState.OpeningTheCardbox:
                PlayAudio(ammoniumExperience_Step5_6_Audio);
                break;
            case AmmoniumExperienceState.SettingTheWavelength:
                PlayAudio(ammoniumExperience_Step6_Audio);
                break;
            case AmmoniumExperienceState.OpeningTheSpectoPhotometer:
                PlayAudio(ammoniumExperience_Step7_Audio);
                break;
            case AmmoniumExperienceState.FillingTheTube:
                PlayAudio(ammoniumExperience_Step8_Audio);
                break;
            case AmmoniumExperienceState.MovingTheTube:
                PlayAudio(ammoniumExperience_Step9_Audio);
                break;
            case AmmoniumExperienceState.ClosingTheSpectoPhotometer:
                break;
            case AmmoniumExperienceState.ShowcasingResults:
                PlayAudio(ammoniumExperience_Step10_Audio);
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
