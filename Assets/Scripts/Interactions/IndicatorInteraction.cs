using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorInteraction : Interaction
{
    [SerializeField] private float waitAnimationTime = 2f;


    private Animator animator;

    private string animationTrigger = "AddDrop";
    public override bool CanInteract { get; set; }


    private void Start()
    {
        animator = GetComponent<Animator>();

        LabManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;

        CanInteract = LabManager.Instance.ExperienceState == ExperienceState.AddingIndicator;
        
    }


    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        UpdateInteractionState();
    }


    public override void Interact()
    {
        CanInteract = false;
        PlayAnimation();
        StartCoroutine(AddDrop());
        
       
    }


    private void PlayAnimation()
    {
        animator.SetTrigger(animationTrigger);
    }

    private IEnumerator AddDrop()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        LabManager.Instance.AddIndicatorDrop();
        UpdateInteractionState();
    }


    private void UpdateInteractionState()
    {
        CanInteract = LabManager.Instance.ExperienceState == ExperienceState.AddingIndicator;
    }
}