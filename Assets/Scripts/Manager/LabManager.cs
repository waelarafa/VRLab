using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Serialization;

public class LabManager : MonoBehaviour
{

    public GameObject mainMenu;
    public static LabManager Instance { get; private set; }


    public event EventHandler OnPhenolphthaleinDropAdded;
    public event EventHandler OnHCIAdded;
    public event EventHandler OnHCIDropAdded;
    public event EventHandler OnMagnetAdded;
    public event EventHandler OnStirringSolution;
    public event EventHandler OnExperienceFinished;

    public event EventHandler OnExperienceChanged;
    public event EventHandler OnExperienceStateChanged;


    private ExperienceState experienceState;


    [SerializeField] private AlkalinityType alkalinityType;

    [Header("Water Materials")] [SerializeField]
    private Material unchangedWaterMaterial;

    [SerializeField] private Material midChangePinkWaterMaterial;
    [SerializeField] private Material midChangeYellowWaterMaterial;
    [SerializeField] private Material ChangedPinkWaterMaterial;
    [SerializeField] private Material ChangedYellowWaterMaterial;
    [SerializeField] private Material ChangedOrangeWaterMaterial;

    [Header("Temporary Mouse Variables")] [SerializeField]
    private Texture2D texture;

    [SerializeField] private Vector2 vector;


    [Header("Water States")] private WaterState waterState;


    private int numberOfIndicatorDrops;
    private int numberOfHCIDrops;
    private int maxNumberOfIndicatorDrops;
    private int maxNumberOfHCIDrops;

    [SerializeField] private bool isGamePaused;

    private void Awake()
    {
        Instance = this;
        //temp mouse
        Cursor.SetCursor(texture, vector, CursorMode.ForceSoftware);

        LoadExperience();

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
            AudioManager.Instance.PauseAudio();
        else
            AudioManager.Instance.ResumeAudio();
    }


    private void LoadAlkalimetricTitrationData()
    {
        alkalinityType = AlkalinityType.AlkalimetricTitration;
        experienceState = ExperienceState.ChargingDropperWithIndicator;
        waterState = WaterState.Unchanged;
        numberOfIndicatorDrops = 0;
        numberOfHCIDrops = 0;
        maxNumberOfIndicatorDrops = 3;
        maxNumberOfHCIDrops = 11;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
        OnExperienceChanged?.Invoke(this, EventArgs.Empty);
    }


    private void LoadCompleteAlkalimetricTitrationData()
    {
        alkalinityType = AlkalinityType.CompleteAlkalimetricTitration;
        experienceState = ExperienceState.AddingIndicator;
        waterState = WaterState.Unchanged;
        numberOfIndicatorDrops = 0;
        numberOfHCIDrops = 0;
        maxNumberOfIndicatorDrops = 3;
        maxNumberOfHCIDrops = 23;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
        OnExperienceChanged?.Invoke(this, EventArgs.Empty);
    }


    private void LoadExperience()
    {
        switch (AlkalinityType)
        {
            case AlkalinityType.AlkalimetricTitration:

                LoadAlkalimetricTitrationData();
                break;
            case AlkalinityType.CompleteAlkalimetricTitration:

                LoadCompleteAlkalimetricTitrationData();
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    public void ChargeDropperWithIndicator()
    {
        experienceState = ExperienceState.AddingIndicator;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }


    public void AddIndicatorDrop()
    {
        numberOfIndicatorDrops++;
        SetWaterState();
        OnPhenolphthaleinDropAdded?.Invoke(this, EventArgs.Empty);

        if (numberOfIndicatorDrops >= maxNumberOfIndicatorDrops)
        {
            experienceState = ExperienceState.PouringWater;
            OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
        }
    }


    public void UpdateWaterColor(GameObject waterObject)
    {
        MeshRenderer renderer = waterObject.GetComponent<MeshRenderer>();


        switch (alkalinityType)
        {
            case AlkalinityType.AlkalimetricTitration:
                switch (waterState)
                {
                    case WaterState.Unchanged:
                        renderer.materials = ChangeMaterial(renderer, unchangedWaterMaterial);
                        break;
                    case WaterState.MidChanging:
                        renderer.materials = ChangeMaterial(renderer, midChangePinkWaterMaterial);
                        break;
                    case WaterState.Changed:
                        renderer.materials = ChangeMaterial(renderer, ChangedPinkWaterMaterial);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                break;
            case AlkalinityType.CompleteAlkalimetricTitration:

                switch (waterState)
                {
                    case WaterState.Unchanged:
                        renderer.materials = ChangeMaterial(renderer, unchangedWaterMaterial);
                        break;
                    case WaterState.MidChanging:
                        renderer.materials = ChangeMaterial(renderer, midChangeYellowWaterMaterial);
                        break;
                    case WaterState.Changed:
                        renderer.materials = ChangeMaterial(renderer, ChangedYellowWaterMaterial);
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }

                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void UpdateWaterColorToOrange(GameObject waterObject)
    {
        MeshRenderer renderer = waterObject.GetComponent<MeshRenderer>();
        renderer.materials = ChangeMaterial(renderer, ChangedOrangeWaterMaterial);
    }


    private Material[] ChangeMaterial(MeshRenderer renderer, Material material)
    {
        if (renderer.materials.Length > 1)
        {
            Material[] currentMaterials = renderer.materials;
            currentMaterials[1] = material;
            return currentMaterials;
        }

        return null;
    }


    public void PourWater()
    {
        experienceState = ExperienceState.AddingCHl;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    private void SetWaterState()
    {
        if (numberOfIndicatorDrops == 1)
            waterState = WaterState.Unchanged;
        else if (numberOfIndicatorDrops == 2)
            waterState = WaterState.MidChanging;
        else
            waterState = WaterState.Changed;
    }

    public void ChargeDropperWithCHl()
    {
        experienceState = ExperienceState.AddingCHl;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }


    public void AddHCI()
    {
        experienceState = ExperienceState.AddingMagnet;
        OnHCIAdded?.Invoke(this, EventArgs.Empty);
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddMagnet()
    {
        experienceState = ExperienceState.MovingTheSolution;
        OnMagnetAdded?.Invoke(this, EventArgs.Empty);
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void MoveSolution()
    {
        experienceState = ExperienceState.ActivatingTheStirrer;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }


    public void StirSolution()
    {
        OnStirringSolution?.Invoke(this, EventArgs.Empty);
        experienceState = ExperienceState.AddingCHlDrops;
        OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
    }

    public void AddHTCDrops()
    {
        numberOfHCIDrops++;
        OnHCIDropAdded?.Invoke(this, EventArgs.Empty);
        if (numberOfHCIDrops >= maxNumberOfHCIDrops)
        {
            waterState = WaterState.Unchanged;
            experienceState = ExperienceState.ShowcasingResults;
            OnExperienceStateChanged?.Invoke(this, EventArgs.Empty);
            OnExperienceFinished?.Invoke(this, EventArgs.Empty);
        }
    }

    public int CalculateAlkalinity() => numberOfHCIDrops * 10;

    public AlkalinityType AlkalinityType
    {
        get => alkalinityType;
        set => alkalinityType = value;
    }

    public ExperienceState ExperienceState
    {
        get => experienceState;
        set => experienceState = value;
    }

    public int NumberOfHciDrops => numberOfHCIDrops;

    public bool IsGamePaused => isGamePaused;
}