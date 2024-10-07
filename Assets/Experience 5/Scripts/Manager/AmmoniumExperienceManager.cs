using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoniumExperienceManager : MonoBehaviour
{
    public static AmmoniumExperienceManager Instance { get; set; }

    public event EventHandler OnExperienceStateChanged;

    private AmmoniumExperienceState experienceState;

    public GameObject mainMenu;

    private bool isGamePaused;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        experienceState = AmmoniumExperienceState.AddingAlkalineSolution;

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

        if (isGamePaused)
            AmmoniumExperienceAudioManager.Instance.PauseAudio();
        else
            AmmoniumExperienceAudioManager.Instance.ResumeAudio();
    }


    public void AddAlkalineSolution()
    {
        experienceState = AmmoniumExperienceState.AddingPhenolNitroprusside;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddPhenolNitroprusside()
    {
        experienceState = AmmoniumExperienceState.AddingTheMagneticBar;

        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddMagneticBar()
    {
        experienceState = AmmoniumExperienceState.MovingTheSolutionToTheStirrer;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void MoveWaterBeakerToStirrer()
    {
        experienceState = AmmoniumExperienceState.TurningOnTheStirrer;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void TurnOnTheStirrer()
    {
        StartCoroutine(AgitateTheSolutionIEnumerator());
    }


    private IEnumerator AgitateTheSolutionIEnumerator()
    {
        experienceState = AmmoniumExperienceState.MixingTheSolution;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(5);
        experienceState = AmmoniumExperienceState.PouringTheSolutionIntoTheTube;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void PourWaterInTube()
    {
        experienceState = AmmoniumExperienceState.ClosingTheTube;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void CloseTheTube()
    {
        experienceState = AmmoniumExperienceState.MovingTheSolutionToTheCardbox;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void MoveTheTube()
    {
        experienceState = AmmoniumExperienceState.ClosingTheCardBox;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void CloseTheCardBox()
    {
        StartCoroutine(HideSolutionFromTheLight());
    }


    private IEnumerator HideSolutionFromTheLight()
    {
        experienceState = AmmoniumExperienceState.CoveringTheSolution;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
        yield return new WaitForSeconds(5);
        experienceState = AmmoniumExperienceState.OpeningTheCardbox;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void OpenTheCardBox()
    {
        experienceState = AmmoniumExperienceState.SettingTheWavelength;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }


    public void SetTheWavelength()
    {
        experienceState = AmmoniumExperienceState.OpeningTheSpectoPhotometer;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void OpenTheSpectoPhotometer()
    {
        experienceState = AmmoniumExperienceState.FillingTheTube;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void FillTheTube()
    {
        experienceState = AmmoniumExperienceState.MovingTheTube;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void MoveTheSpectoTube()
    {
        experienceState = AmmoniumExperienceState.ClosingTheSpectoPhotometer;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void CloseTheSpectoPhotometer()
    {
        experienceState = AmmoniumExperienceState.ShowcasingResults;
        StartCoroutine(WaitingForResults());
    }

    private IEnumerator WaitingForResults()
    {
        yield return new WaitForSeconds(4);
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public AmmoniumExperienceState ExperienceState => experienceState;
    public bool IsGamePaused => isGamePaused;
}