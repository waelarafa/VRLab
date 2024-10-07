using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AscorbicAcidFlask : Interaction
{
     public override bool CanInteract { get; set; }

    [SerializeField] private float waitAnimationTime = 2f;

    private Animator animator;
    
    private string animationTrigger = "ChargeDropper";
    private string addDropTrigger = "AddDrop";

    private bool isCharged;

    private void Start()
    {
        animator = GetComponent<Animator>();

        isCharged = true;
        
       CanInteract = SpectoLabManager.Instance.ExperienceState == SpectoExperienceState.AddingAscorbicAcid;
       
       SpectoLabManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
       
    }
    
    
    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        CanInteract = SpectoLabManager.Instance.ExperienceState == SpectoExperienceState.AddingAscorbicAcid;
    }
    
    

    private IEnumerator AddDrop()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        SpectoLabManager.Instance.AddAcidDrop();
    }


    public override void Interact()
    {
        if (isCharged)
        {
            animator.SetTrigger(addDropTrigger);
            StartCoroutine(AddDrop());
        }
    }
    
    
    
    
}
