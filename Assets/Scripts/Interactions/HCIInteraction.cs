using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class HCIInteraction : Interaction
{
    [SerializeField] private float waitAnimationTime = 1f;


    private Animator animator;


    private string animationTrigger = "AddHCI";
    
    private string chlAnimationTrigger = "AddCHl";

    public override bool CanInteract { get; set; }

    private void Start()
    {
        animator = GetComponent<Animator>();
        LabManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
        
    }


    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        UpdateInteractionState();
    }

    private void UpdateInteractionState()
    {
        CanInteract = LabManager.Instance.ExperienceState == ExperienceState.AddingCHl;
    }


    public override void Interact()
    {
        CanInteract = false;
        PlayAnimation();
        StartCoroutine(AddDrop());
    }

    private void PlayAnimation()
    {
        switch (LabManager.Instance.AlkalinityType)
        {
            case AlkalinityType.AlkalimetricTitration:
                animator.SetTrigger(animationTrigger);
                break;
            case AlkalinityType.CompleteAlkalimetricTitration:
                animator.SetTrigger(chlAnimationTrigger);
                
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
        
    }

    private IEnumerator AddDrop()
    {
        yield return new WaitForSeconds(1f);
        LabManager.Instance.AddHCI();
    }
}