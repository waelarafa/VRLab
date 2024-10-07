using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpectoWaterInteraction : Interaction
{
    [SerializeField] private float waitAnimationTime = 0.75f;
    [SerializeField] private GameObject waterObject;
    [SerializeField] private GameObject highlightObject;

    private string animationTrigger = "PourWater";
    private Animator animator;
    public override bool CanInteract { get; set; }


    private void Start()
    {
        animator = GetComponent<Animator>();

        SpectoLabManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }


    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        UpdateInteractionState();
        UpdateWaterContainer();
    }


    private void UpdateInteractionState()
    {
        CanInteract = SpectoLabManager.Instance.ExperienceState == SpectoExperienceState.PouringTheSolution;
    }


    public override void Interact()
    {
        highlightObject.SetActive(false);
        CanInteract = false;
        PlayAnimation();
        StartCoroutine(PourWater());
    }

    private void PlayAnimation()
    {
        animator.SetTrigger(animationTrigger);
    }

    private IEnumerator PourWater()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        SpectoLabManager.Instance.PourWater();
    }


    private void UpdateWaterContainer()
    {
        if (SpectoLabManager.Instance.ExperienceState != SpectoExperienceState.AddingAscorbicAcid ||
            SpectoLabManager.Instance.ExperienceState != SpectoExperienceState.AddingCombinedReagent ||
            SpectoLabManager.Instance.ExperienceState != SpectoExperienceState.PouringTheSolution)
        {
            waterObject.SetActive(false);
        }
    }
}