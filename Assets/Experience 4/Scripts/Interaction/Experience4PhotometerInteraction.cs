using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience4PhotometerInteraction : Interaction
{
    public override bool CanInteract { get; set; }
    
    [SerializeField] private float waitAnimationTime = 2f;
    
    private string openTrigger = "OpenSpecto";
    private string closeTrigger = "CloseSpecto";
    
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        CanInteract = Experience4Manager.Instance.ExperienceState == Experience4State.OpeningTheSpectoPhotometer;
        Experience4Manager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        InteractableObject interactableObject = GetComponent<InteractableObject>();
        
        CanInteract = Experience4Manager.Instance.ExperienceState == Experience4State.OpeningTheSpectoPhotometer || 
                      Experience4Manager.Instance.ExperienceState == Experience4State.ClosingTheSpectoPhotometer;

        if (Experience4Manager.Instance.ExperienceState == Experience4State.ClosingTheSpectoPhotometer)
        {
            interactableObject.ItemActionText = "Close the spectophotometer cover";
        }
        
    }
    
    private IEnumerator OpenPhotometer()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        Experience4Manager.Instance.OpeningTheSpectoPhotometer();
    }
    
    private IEnumerator ClosePhotometer()
    {
        CanInteract = false;
        yield return new WaitForSeconds(waitAnimationTime);
        Experience4Manager.Instance.ClosingThePhotometer();
    }
    
    
    public override void Interact()
    {
        if (Experience4Manager.Instance.ExperienceState == Experience4State.OpeningTheSpectoPhotometer)
        {
            animator.SetTrigger(openTrigger);
            StartCoroutine(OpenPhotometer());
        }
        else
        if (Experience4Manager.Instance.ExperienceState == Experience4State.ClosingTheSpectoPhotometer)
        {
            animator.SetTrigger(closeTrigger);
            StartCoroutine(ClosePhotometer());
        }
        
       
    }
}
