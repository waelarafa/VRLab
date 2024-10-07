using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StirButtonInteraction : Interaction
{
    public override bool CanInteract { get; set; }

    private string turnOnAnimationTrigger = "TurnOn";
    private string turnOffAnimationTrigger = "TurnOff";
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        LabManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
        LabManager.Instance.OnExperienceChanged += LabManager_OnExperienceChanged;
    }

    private void LabManager_OnExperienceChanged(object sender, EventArgs e)
    {
        animator.SetTrigger(turnOffAnimationTrigger); 
    }


    public override void Interact()
    {
        animator.SetTrigger(turnOnAnimationTrigger);
        LabManager.Instance.StirSolution();
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        UpdateInteractionState();
    }


    private void UpdateInteractionState()
    {
        CanInteract = LabManager.Instance.ExperienceState == ExperienceState.ActivatingTheStirrer;
    }
}