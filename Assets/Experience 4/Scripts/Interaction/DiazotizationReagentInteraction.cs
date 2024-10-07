using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiazotizationReagentInteraction : Interaction
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
        
        CanInteract =  Experience4Manager.Instance.ExperienceState == Experience4State.AddingDiazotizationReagent;
       
        Experience4Manager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
       
    }
    
    
    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        CanInteract = Experience4Manager.Instance.ExperienceState == Experience4State.AddingDiazotizationReagent;
    }
    
    

    private IEnumerator AddDrop()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        Experience4Manager.Instance.AddDiazotizationReagentDrop();
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
