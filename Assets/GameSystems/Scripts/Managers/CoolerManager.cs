using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolerManager : MonoBehaviour
{
    public static CoolerManager Instance { get; set; }

    [SerializeField] private GameObject coolerArea1;
    [SerializeField] private GameObject coolerArea2;
    [SerializeField] private GameObject coolerArea3;
    [SerializeField] private GameObject closedCoolerArea1;
    [SerializeField] private GameObject closedCoolerArea2;
    [SerializeField] private GameObject closedCoolerArea3;

    private GameObject activeCooler;
    private GameObject activeClosedCooler;

    private int nbOfBottles = 0;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }

    private void Start()
    {
        activeCooler = coolerArea1;
    }


    private void UpdateCoolerStates()
    {
        switch (nbOfBottles)
        {
            case 3:
                activeCooler = coolerArea2;
                activeClosedCooler = closedCoolerArea1; 
                coolerArea1.SetActive(false);
                closedCoolerArea1.SetActive(true);
                break;
            case 6:
                activeCooler = coolerArea3;
                activeClosedCooler = closedCoolerArea2; 
                coolerArea2.SetActive(false);
                closedCoolerArea2.SetActive(true);
                break;
            case 9:
                activeCooler = coolerArea1; 
                activeClosedCooler = closedCoolerArea3; 
                coolerArea3.SetActive(false);
                closedCoolerArea3.SetActive(true);
                break;
        }
    }


    private void UpdateCoolerVisual()
    {
        Cooler cooler = activeCooler.GetComponent<Cooler>();


        for (int i = 0; i < nbOfBottles; i++)
        {
            cooler.BottleArray[i].SetActive(true);
        }
    }

    public void AddBottle()
    {
        nbOfBottles++;
        UpdateCoolerStates();
        UpdateCoolerVisual();
    }


    public void MoveCooler()
    {
        if (activeClosedCooler == closedCoolerArea1)
        {
            closedCoolerArea1.SetActive(false);
            coolerArea2.SetActive(true);
        }
        else
        if (activeClosedCooler == closedCoolerArea2)
        {
            closedCoolerArea2.SetActive(false);
            coolerArea3.SetActive(true);
        }
    }
    
    public int NbOfBottles
    {
        get => nbOfBottles;
        set => nbOfBottles = value;
    }
    
}