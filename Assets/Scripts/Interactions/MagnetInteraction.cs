using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MagnetInteraction : Interaction
{
    public override bool CanInteract { get; set; }
    
    [SerializeField] private float waitAnimationTime = 1f;
    private string animationTrigger = "MoveMagnet";
    
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        LabManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
        LabManager.Instance.OnExperienceChanged += LabManager_OnExperienceChanged;
    }

    private void LabManager_OnExperienceChanged(object sender, EventArgs e)
    {
        gameObject.SetActive(true);
    }


    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        UpdateInteractionState();
    }
    
    private void UpdateInteractionState()
    {
        CanInteract = LabManager.Instance.ExperienceState == ExperienceState.AddingMagnet; 
    }
    
    public override void Interact()
    {
       animator.SetTrigger(animationTrigger);
       StartCoroutine(AddMagnet());
    }
    
    
    private IEnumerator AddMagnet()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        LabManager.Instance.AddMagnet();
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }


    
    
    
}


