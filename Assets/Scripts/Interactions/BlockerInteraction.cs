using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockerInteraction : Interaction
{
    public override bool CanInteract { get; set; }

    private string addDropAnimationTrigger = "AddHCIDrop";

    private Animator animator;

    private void Start()
    {
        LabManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;

        animator = GetComponent<Animator>();
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        UpdateInteractionState();
    }


    private void UpdateInteractionState()
    {
        CanInteract = LabManager.Instance.ExperienceState == ExperienceState.AddingCHlDrops;
    }


    public override void Interact()
    {
        animator.SetTrigger(addDropAnimationTrigger);
        LabManager.Instance.AddHTCDrops();
    }
}