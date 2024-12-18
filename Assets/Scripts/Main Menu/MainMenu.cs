using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{

    [SerializeField] private GameObject labButtons;
    [SerializeField] private GameObject SampleAnalysisButton;

    private string PhenolphthaleinAlkaliniyExperieceName = "Phenolphthalein Alkalnity";
    private string CompletePhenolphthaleinAlkaliniyExperieceName = "MythOrange";
    private string RiverSceneName = "River";
    private string Ammonium = "AmmoniumVR";
    private string SpectroVR = "SpectroVR";
    private string Nitrites = "NitritesV2";
    private string MaterialPreparation = "EquipmentCollect";
    private string Sampling = "Sampling";
    private string mainMenu = "MainMenu";
    public void ShowLabButtons()
    {
        labButtons.SetActive(true);
    }
    public void ShowSampleAnalysisButtons()
    {
        SampleAnalysisButton.SetActive(true);
    }
    public void LoadMaterialPreparationScene()
    {
        SceneManager.LoadScene(MaterialPreparation);
    }
    public void LoadSamplingScene()
    {
        SceneManager.LoadScene(Sampling);
    }
    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(mainMenu);
    }
    


    public void LoadPhenolphthaleinAlkalinityExperience()
    {
        SceneManager.LoadScene(PhenolphthaleinAlkaliniyExperieceName);
    }
    
    public void LoadCompleteinAlkalnityExperience()
    {
        SceneManager.LoadScene(CompletePhenolphthaleinAlkaliniyExperieceName);
    }
    public void LoadOrthophosphateExperience()
    {
        SceneManager.LoadScene(SpectroVR);
    }
    public void LoadAmmoniumExperience()
    {
        SceneManager.LoadScene(Ammonium);
    }
    public void LoadNitritesExperience()
    {
        SceneManager.LoadScene(Nitrites);
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
