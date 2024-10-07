using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectoLabManager : MonoBehaviour
{
    public static SpectoLabManager Instance { get; private set; }

    public event EventHandler OnExperienceStateChanged;
    
    private SpectoExperienceState experienceState;

    [SerializeField] private Material lightBlueLiquidMaterial;

    public GameObject mainMenu;

    private bool isGamePaused;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        experienceState = SpectoExperienceState.AddingAscorbicAcid;

        isGamePaused = false;
    }


    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && mainMenu)
        {
            Pause();
        }
    }

    public void Pause()
    {
        mainMenu.SetActive(!mainMenu.activeSelf);
        isGamePaused = mainMenu.activeSelf;
           
        if(isGamePaused)
            SpectoAudioManager.Instance.PauseAudio();
        else
            SpectoAudioManager.Instance.ResumeAudio();
    }
    
    
    public void AddAcidDrop()
    {
        

        experienceState = SpectoExperienceState.AddingCombinedReagent;

        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }


    public void AddReagent()
    {
        experienceState = SpectoExperienceState.PouringTheSolution;
    
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }


    public void PourWater()
    {
        experienceState = SpectoExperienceState.MovingTheSolutionToTheMixer;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void MoveSolutionToTheMixer()
    {
        experienceState = SpectoExperienceState.TurningOnTheMixer;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }


    public void AgitateTheSolution()
    {
        StartCoroutine(AgitateTheSolutionIEnumerator());
    }


    private IEnumerator AgitateTheSolutionIEnumerator()
    {
        experienceState = SpectoExperienceState.AgitatingTheSolution;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(5);
        experienceState = SpectoExperienceState.MovingTheSolutionToTheRack;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }


    public void MoveSolutionToTheRack()
    {
        experienceState = SpectoExperienceState.RestingTheSolution;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
        RestingTheSolution();
    }


    public void RestingTheSolution()
    {
        StartCoroutine(RestingTheSolutionIEnumerator());
    }


    private IEnumerator RestingTheSolutionIEnumerator()
    {
        yield return new WaitForSeconds(20);
        experienceState = SpectoExperienceState.SettingTheWavelength;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }


    public void SettingTheWavelength()
    {
        experienceState = SpectoExperienceState.OpeningTheSpectoPhotometer;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void OpeningTheSpectoPhotometer()
    {
        experienceState = SpectoExperienceState.FillingTheTube;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void FillingTheTube()
    {
        experienceState = SpectoExperienceState.MovingTheTube;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void MovingTheTube()
    {
        experienceState = SpectoExperienceState.ClosingTheSpectoPhotometer;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ClosingThePhotometer()
    {
        experienceState = SpectoExperienceState.ShowcasingResults;
        StartCoroutine(WaitingForResults()); 
    }
    
     private IEnumerator WaitingForResults()
        {
            yield return new WaitForSeconds(4);
            OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
        }
    

    public SpectoExperienceState ExperienceState
    {
        get => experienceState;
        set => experienceState = value;
    }

    public Material LightBlueLiquidMaterial => lightBlueLiquidMaterial;

    public bool IsGamePaused => isGamePaused;
}