using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LabUIManager : MonoBehaviour
{
    public static LabUIManager Instance { get; private set; }

    [SerializeField] private TMP_InputField concentrationText;
    [SerializeField] private TMP_InputField volumeText;
    [SerializeField] private TMP_InputField phText;
    [SerializeField] private TMP_InputField addedVolumeText;
    [SerializeField] private TMP_Dropdown indicatorDropdown;

    [SerializeField] private TextMeshProUGUI actionText;
    [SerializeField] private TextMeshProUGUI experienceNameText;
    [SerializeField] private TextMeshProUGUI experienceFormula;
    [SerializeField] private TextMeshProUGUI currentFormulaResults;

    [SerializeField] private GameObject resultScreen;
    [SerializeField] private TextMeshProUGUI resultScreenText;

    private void Awake()
    {
        Instance = this;
        ClearActionText();
       currentFormulaResults.gameObject.SetActive(false);
    }


    private void Start()
    {
       LabManager.Instance.OnExperienceChanged += LabManager_OnExperienceChanged;
        LabManager.Instance.OnExperienceFinished += LabManager_OnExperienceFinished;
        LabManager.Instance.OnHCIDropAdded += InstanceOnOnHCIDropAdded;
        SetDefaultText();
        SetIndicator();
        SetExperienceNameText();
        SetExperienceFormula();
        SetCurrentExperienceResults();
        resultScreen.SetActive(false);
    }

    private void InstanceOnOnHCIDropAdded(object sender, EventArgs e)
    {
        addedVolumeText.text = LabManager.Instance.NumberOfHciDrops + " ml";
        SetCurrentExperienceResults();
    }

    private void SetDefaultText()
    {
        concentrationText.text = "0.02";
        volumeText.text = "50";
        phText.text = "0";
        addedVolumeText.text = "0 ml";
    }

    private void SetIndicator()
    {
        switch (LabManager.Instance.AlkalinityType)
        {
            case AlkalinityType.AlkalimetricTitration:
                indicatorDropdown.value = 0;
                break;
            case AlkalinityType.CompleteAlkalimetricTitration:
                indicatorDropdown.value = 1;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void LabManager_OnExperienceFinished(object sender, EventArgs e)
    {
        resultScreen.SetActive(true);
        SetCurrentExperienceResultsText();
        currentFormulaResults.gameObject.SetActive(true);
    }

    private void LabManager_OnExperienceChanged(object sender, EventArgs e)
    {
        SetIndicator();
        SetExperienceNameText();
        SetExperienceFormula();
    }


    public void SetActionText(string text)
    {
        actionText.text = text;
    }

    public void ClearActionText()
    {
        actionText.text = "";
    }


    private void SetExperienceNameText()
    {
        switch (LabManager.Instance.AlkalinityType)
        {
            case AlkalinityType.AlkalimetricTitration:
                experienceNameText.text = "Phenolphthalein Alkalinity";
                break;
            case AlkalinityType.CompleteAlkalimetricTitration:
                experienceNameText.text = "Total Alkalinity";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetExperienceFormula()
    {
        switch (LabManager.Instance.AlkalinityType)
        {
            case AlkalinityType.AlkalimetricTitration:
                experienceFormula.text = "P. Alkalinity = ml of acid x 10 ppm/ml.";
                break;
            case AlkalinityType.CompleteAlkalimetricTitration:
                experienceFormula.text = "TA = ml of acid x 10 ppm/ml.";
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetCurrentExperienceResults()
    {
        switch (LabManager.Instance.AlkalinityType)
        {
            case AlkalinityType.AlkalimetricTitration:
                currentFormulaResults.text = "P. Alkalinity = " + LabManager.Instance.NumberOfHciDrops +
                                             " ml of acid x 10 ppm/ml = " + LabManager.Instance.CalculateAlkalinity() +
                                             " ppm";
                break;
            case AlkalinityType.CompleteAlkalimetricTitration:
                currentFormulaResults.text = "TA = " + LabManager.Instance.NumberOfHciDrops +
                                             " ml of acid x 10 ppm/ml = " + LabManager.Instance.CalculateAlkalinity() +
                                             " ppm";
                ;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    private void SetCurrentExperienceResultsText()
    {
        switch (LabManager.Instance.AlkalinityType)
        {
            case AlkalinityType.AlkalimetricTitration:
                resultScreenText.text =
                    "The water’s color has changed back to its original state, the volume added of HCl is 11 ml, and Phenolphthalein Alkalinity is 110 ppm.";

                phText.text = "8.3";
                
                break;
            case AlkalinityType.CompleteAlkalimetricTitration:
                resultScreenText.text =
                    "The water’s color has changed to orange, the volume added of HCl is 23 ml, and total Alkalinity is 230 ppm";
               
                phText.text = "4.5";
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Main Menu");
    }
}