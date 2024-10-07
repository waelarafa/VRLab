using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience4Manager : MonoBehaviour
{
    public static Experience4Manager Instance { get; private set; }

    public event EventHandler OnExperienceStateChanged;

    private Experience4State experienceState;

    [SerializeField] private Material diazotizationReagentColor;
    [SerializeField] private Material diazotizatedWaterColor;
    [SerializeField] private Material diazotizatedSolutionColor;

    public GameObject mainMenu;

    [SerializeField] private bool isGamePaused;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }

        experienceState = Experience4State.AddingDiazotizationReagent;
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
           
        if(IsGamePaused)
            Experience4AudioManager.Instance.PauseAudio();
        else
            Experience4AudioManager.Instance.ResumeAudio();
    }
    
    

    public void AddDiazotizationReagentDrop()
    {
        experienceState = Experience4State.PouringTheSolution;

        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }
    
    public void PourWater()
    {
        experienceState = Experience4State.MovingTheSolutionToTheMixer;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    
       public void MoveSolutionToTheMixer()
    {
        experienceState = Experience4State.TurningOnTheMixer;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }


    public void AgitateTheSolution()
    {
        StartCoroutine(AgitateTheSolutionIEnumerator());
    }


    private IEnumerator AgitateTheSolutionIEnumerator()
    {
        experienceState = Experience4State.AgitatingTheSolution;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(5);
        experienceState = Experience4State.MovingTheSolutionToTheRack;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }


    public void MoveSolutionToTheRack()
    {
        experienceState = Experience4State.RestingTheSolution;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
        RestingTheSolution();
    }
    
    public void RestingTheSolution()
    {
        StartCoroutine(RestingTheSolutionIEnumerator());
    }
    
    private IEnumerator RestingTheSolutionIEnumerator()
    {
        yield return new WaitForSeconds(5);
        experienceState = Experience4State.SettingTheWavelength;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }


    public void SettingTheWavelength()
    {
        experienceState = Experience4State.OpeningTheSpectoPhotometer;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void OpeningTheSpectoPhotometer()
    {
        experienceState = Experience4State.FillingTheTube;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void FillingTheTube()
    {
        experienceState = Experience4State.MovingTheTube;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void MovingTheTube()
    {
        experienceState = Experience4State.ClosingTheSpectoPhotometer;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void ClosingThePhotometer()
    {
        experienceState = Experience4State.ShowcasingResults;
        StartCoroutine(WaitingForResults()); 
    }
    
     private IEnumerator WaitingForResults()
        {
            yield return new WaitForSeconds(4);
            OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
        }
    
    
    public Experience4State ExperienceState => experienceState;

    public Material DiazotizationReagentColor => diazotizationReagentColor;

    public Material DiazotizatedWaterColor => diazotizatedWaterColor;

    public Material DiazotizatedSolutionColor => diazotizatedSolutionColor;

    public bool IsGamePaused => isGamePaused;
}