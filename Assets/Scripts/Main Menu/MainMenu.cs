using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject labButtons;
    [SerializeField] private GameObject SampleAnalysisButton;

    private string PhenolphthaleinAlkaliniyExperieceName = "Alkalimetric Titration Experience";
    private string CompletePhenolphthaleinAlkaliniyExperieceName = "Complete Alkalimetric Titration Experience";
    private string RiverSceneName = "River";

    public void ShowLabButtons()
    {
        labButtons.SetActive(true);
    }


    public void ShowSampleAnalysisButtons()
    {
        SampleAnalysisButton.SetActive(true);
    }


    public void LoadPhenolphthaleinAlkalinityExperience()
    {
        SceneManager.LoadScene(PhenolphthaleinAlkaliniyExperieceName);
    }
    
    public void LoadCompleteinAlkalnityExperience()
    {
        SceneManager.LoadScene(CompletePhenolphthaleinAlkaliniyExperieceName);
    }

    public void Quit()
    {
        Application.Quit();
    }

    public void LoadRiverWaterSamplingScene()
    {
        SceneManager.LoadScene(RiverSceneName);
    }
    
    
}
