using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SpectoUIManager : MonoBehaviour
{
    public static SpectoUIManager Instance { get; private set; }

    [SerializeField] public TextMeshProUGUI phenolphthaleinVolume;


    [SerializeField] private TMP_Text resultText;
    [SerializeField] private GameObject resultTextGameObject;
    [SerializeField] private TMP_Text initialText;
    [SerializeField] private GameObject initialTextGameObject;
    [SerializeField] private GameObject mainMenuButtonGameObject;
    [SerializeField] private TextMeshProUGUI actionText;


    private void Awake()
    {
        Instance = this;
        ClearActionText();
    }

    private void Start()
    {
        SpectoLabManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;


        initialText.text =
            "Orthophosphates exist in three forms: orthophosphate, metaphosphate (or polyphosphate) and organically bound phosphate. Orthophosphates are used on agricultural land as fertilizers. " +
            "They enter surface waters during rains or melting snow. " + "Polyphosphates are used in laundry detergents and synthetic detergents.\n" +
            "In order to determine the presence of orthophosphate in water we must follow these steps: firstly take 10 ml of water sample.";
        
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
        if (SpectoLabManager.Instance.ExperienceState == SpectoExperienceState.OpeningTheSpectoPhotometer)
        {
            resultTextGameObject.SetActive(true);
            resultText.text = "Spectrophotometer wavelength set to 700nm";
        }
        else if (SpectoLabManager.Instance.ExperienceState == SpectoExperienceState.ShowcasingResults)
        {
            mainMenuButtonGameObject.SetActive(true);
            resultTextGameObject.SetActive(true);
            resultText.text =
                "The absorbance of spectrometric test solution (OD) is 0.035 nm. " +
                "The orthophosphate concentration [PO<sub>4</sub><sup>-3</sup>] of the sample," +
                " expressed in milligrams per liter, is given by the expression: \n " +
                "[PO<sub>4</sub><sup>-3</sup>]= (6,652 x OD) + 0,0247 = 0.257 mg/l";
        }

        else
        {
            resultTextGameObject.SetActive(false);
            initialTextGameObject.SetActive(false);
        }
    }


    public GameObject InitialTextGameObject
    {
        get => initialTextGameObject;
        set => initialTextGameObject = value;
    }
}
