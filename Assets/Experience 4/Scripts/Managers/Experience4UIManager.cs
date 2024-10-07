using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Experience4UIManager : MonoBehaviour
{
    public static Experience4UIManager Instance { get; private set; }

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
        Experience4Manager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
        initialText.text =
            "Nitrites are chemical compounds that contain the nitrite ion (NO<sub>2-</sub>). " +
            "They are used in various applications, including as preservatives in processed meats to inhibit bacterial growth and maintain color. " +
            "In order to determine the presence of nitrites in water by using AFNOR T 90-013 method, please follow these steps:\n" +
            "firstly take 25 ml of water sample. After add 0.5 ml of Diazotization reagent";

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
        if (Experience4Manager.Instance.ExperienceState == Experience4State.OpeningTheSpectoPhotometer)
        {
            resultTextGameObject.SetActive(true);
            resultText.text = "Spectrophotometer wavelength set to 537nm";
        }
        else if (Experience4Manager.Instance.ExperienceState == Experience4State.ShowcasingResults)
        {
            mainMenuButtonGameObject.SetActive(true);
            resultTextGameObject.SetActive(true);
            resultText.text =
                "The absorbance of spectrometric test solution (OD) is 1.783 nm. " +
                "The Nitrite concentration [NO<sub>2-</sub>] of the sample, expressed in milligrams per liter, is given by the expression:\n" +
                "[NO<sub>2-</sub>] = (0,661 x OD) + 0,0084 = 1.186 mg/l";
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