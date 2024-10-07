using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;

public class WaterInteraction : Interaction
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

        LabManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
        LabManager.Instance.OnExperienceChanged += LabManager_OnExperienceChanged;

        LabManager.Instance.OnPhenolphthaleinDropAdded += LabManager_OnPhenolphthaleinDropAdded;
    }
    

    private void LabManager_OnExperienceChanged(object sender, EventArgs e)
    {
        LabManager.Instance.UpdateWaterColor(waterObject);
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        UpdateInteractionState();
        UpdateWaterContainer();
    }


    private void UpdateInteractionState()
    {
        CanInteract = LabManager.Instance.ExperienceState == ExperienceState.PouringWater;
    }

    private void LabManager_OnPhenolphthaleinDropAdded(object sender, EventArgs e)
    {
        LabManager.Instance.UpdateWaterColor(waterObject);
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
        LabManager.Instance.PourWater();
    }


    private void UpdateWaterContainer()
    {
        if (LabManager.Instance.ExperienceState != ExperienceState.AddingIndicator ||
            LabManager.Instance.ExperienceState != ExperienceState.PouringWater)
        {
            waterObject.SetActive(false);
        }
    }
}