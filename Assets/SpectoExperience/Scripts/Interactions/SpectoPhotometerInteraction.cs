using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectoPhotometerInteraction : Interaction
{
    public override bool CanInteract { get; set; }
    
    [SerializeField] private float waitAnimationTime = 2f;
    
    private string openTrigger = "OpenSpecto";
    private string closeTrigger = "CloseSpecto";
    
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        CanInteract = SpectoLabManager.Instance.ExperienceState == SpectoExperienceState.OpeningTheSpectoPhotometer;
        SpectoLabManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        InteractableObject interactableObject = GetComponent<InteractableObject>();
        
        CanInteract = SpectoLabManager.Instance.ExperienceState == SpectoExperienceState.OpeningTheSpectoPhotometer || 
                      SpectoLabManager.Instance.ExperienceState == SpectoExperienceState.ClosingTheSpectoPhotometer;
        
        
        if (SpectoLabManager.Instance.ExperienceState == SpectoExperienceState.ClosingTheSpectoPhotometer)
        {
            interactableObject.ItemActionText = "Close the spectophotometer cover";
        }
    }
    
    private IEnumerator OpenPhotometer()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        SpectoLabManager.Instance.OpeningTheSpectoPhotometer();
    }
    
    private IEnumerator ClosePhotometer()
    {
        
        CanInteract = false; 
        yield return new WaitForSeconds(waitAnimationTime);
       SpectoLabManager.Instance.ClosingThePhotometer();
    }
    
    
    public override void Interact()
    {
        if (SpectoLabManager.Instance.ExperienceState == SpectoExperienceState.OpeningTheSpectoPhotometer)
        {
            animator.SetTrigger(openTrigger);
            StartCoroutine(OpenPhotometer());
        }
        else
        if (SpectoLabManager.Instance.ExperienceState == SpectoExperienceState.ClosingTheSpectoPhotometer)
        {
            animator.SetTrigger(closeTrigger);
            StartCoroutine(ClosePhotometer());
        }
        
       
    }
}