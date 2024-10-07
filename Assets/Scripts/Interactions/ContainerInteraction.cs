using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ContainerInteraction : Interaction
{
    [SerializeField] private GameObject waterObject;
    [SerializeField] private GameObject magnetObject;
    [SerializeField] private GameObject highlightMaterial;

    private string fillAnimationTrigger = "FillContainer";
    private string moveAnimationTrigger = "MoveContainer";
    private string addMagnetAnimationTrigger = "AddMagnet";
    private string rotateContainerAnimationTrigger = "RotateContainer";
    private Animator animator;

    public override bool CanInteract { get; set; }


    private void Start()
    {
        animator = GetComponent<Animator>();

        LabManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
        LabManager.Instance.OnMagnetAdded += LabManager_OnMagnetAdded;
        LabManager.Instance.OnStirringSolution += LabManager_OnStirSolution;
        LabManager.Instance.OnExperienceFinished += LabManager_OnExperienceFinished;
        LabManager.Instance.OnExperienceChanged += LabManager_OnExperienceChanged;
    }

    private void LabManager_OnExperienceChanged(object sender, EventArgs e)
    {
        waterObject.SetActive(false);
        magnetObject.SetActive(false);
    }


    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        if (LabManager.Instance.ExperienceState == ExperienceState.AddingCHl)
        {
            FillContainer();
        }

        UpdateInteractionState();
    }


    private void LabManager_OnMagnetAdded(object sender, EventArgs e)
    {
        magnetObject.SetActive(true);
        animator.SetTrigger(addMagnetAnimationTrigger);
    }

    private void LabManager_OnStirSolution(object sender, EventArgs e)
    {
        animator.SetTrigger(rotateContainerAnimationTrigger);
    }


    private void LabManager_OnExperienceFinished(object sender, EventArgs e)
    {
        switch (LabManager.Instance.AlkalinityType)
        {
            case AlkalinityType.AlkalimetricTitration:
                LabManager.Instance.UpdateWaterColor(waterObject);
                break;
            case AlkalinityType.CompleteAlkalimetricTitration:
                LabManager.Instance.UpdateWaterColorToOrange(waterObject);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }


    private void UpdateInteractionState()
    {
        CanInteract = LabManager.Instance.ExperienceState == ExperienceState.MovingTheSolution;
    }


    private void FillContainer()
    {
        animator.SetTrigger(fillAnimationTrigger);
       LabManager.Instance.UpdateWaterColor(waterObject);
        waterObject.SetActive(true);
    }


    public override void Interact()
    {
        animator.SetTrigger(moveAnimationTrigger);
        LabManager.Instance.MoveSolution();
        highlightMaterial.gameObject.SetActive(false);
    }
}