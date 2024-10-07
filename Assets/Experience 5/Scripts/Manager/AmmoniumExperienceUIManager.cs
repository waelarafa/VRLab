using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AmmoniumExperienceUIManager : MonoBehaviour
{
    public static AmmoniumExperienceUIManager Instance { get; set; }

    [Header("Result Text")] [SerializeField]
    private TMP_Text resultText;

    [SerializeField] private GameObject resultTextGameObject;

    [Header("Initial Text")] [SerializeField]
    private TMP_Text initialText;

    [SerializeField] private GameObject initialTextGameObject;
    [Header("Main Menu")] [SerializeField] private GameObject mainMenuButtonGameObject;

    [Header("Action Text")] [SerializeField]
    private TextMeshProUGUI actionText;

    private void Awake()
    {
        if (Instance == null)
            Instance = this;

        ClearActionText();
    }

    private void Start()
    {
        AmmoniumExperienceManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;

        initialText.text =
            "The determination of Ammonium concentration in the laboratory is an analytical technique used by students to measure " +
            "the amount of ammonium ions in a solution. This analysis is important in various fields such as water " +
            "quality control, agriculture, and the chemical industry. To determine the concentration of Ammonium " +
            "in a sample water we must follow those steps: \n" +
            "firstly take 10 ml of water sample and add 0.5 ml of Alkaline solution.";

        initialTextGameObject.SetActive(true);

        mainMenuButtonGameObject.SetActive(false);
    }

    public void SetActionText(string text)
    {
        actionText.text = text;
    }

    public void ClearActionText()
    {
        actionText.text = "";
    }


    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.OpeningTheSpectoPhotometer)
        {
            resultTextGameObject.SetActive(true);
            resultText.text = "Spectrophotometer wavelength set to 630nm";
        }
        else if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.ShowcasingResults)
        {
            mainMenuButtonGameObject.SetActive(true);
            resultTextGameObject.SetActive(true);
            resultText.text =
                "The absorbance of spectrometric test solution (OD) is 9.87 nm. " +
                "The Ammonium concentration [NH<sub>4+</sub>] of the sample, expressed in milligrams per liter, is given by the expression:\n"+
                "[NH<sub>4+</sub>] = (0,652 x OD) + 0,0247 = 65.68 mg/l.";
        }

        else
        {
            initialTextGameObject.SetActive(false);
            resultTextGameObject.SetActive(false);
        }
    }

    public GameObject InitialTextGameObject
    {
        get => initialTextGameObject;
        set => initialTextGameObject = value;
    }
}