using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorDropperInteraction : Interaction
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

        switch (LabManager.Instance.AlkalinityType)
        {
            case AlkalinityType.AlkalimetricTitration:
                CanInteract = LabManager.Instance.ExperienceState == ExperienceState.ChargingDropperWithIndicator;
                isCharged = false;
                break;
            case AlkalinityType.CompleteAlkalimetricTitration:
                CanInteract = LabManager.Instance.ExperienceState == ExperienceState.AddingIndicator;
                isCharged = true;
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }

        LabManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }


    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        CanInteract = LabManager.Instance.ExperienceState == ExperienceState.ChargingDropperWithIndicator ||
                      LabManager.Instance.ExperienceState == ExperienceState.AddingIndicator;

        if (LabManager.Instance.ExperienceState == ExperienceState.AddingIndicator)
        {
            InteractableObject interactableObject = GetComponent<InteractableObject>();

            switch (LabManager.Instance.AlkalinityType)
            {
                case AlkalinityType.AlkalimetricTitration:
                    interactableObject.ItemActionText = "Add a Phenolphthalein drop";
                    break;
                case AlkalinityType.CompleteAlkalimetricTitration:
                    interactableObject.ItemActionText = "Add a Methyl orange drop.";
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }

    private IEnumerator AddDrop()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        LabManager.Instance.AddIndicatorDrop();
    }


    public override void Interact()
    {
        if (isCharged)
        {
            animator.SetTrigger(addDropTrigger);
            StartCoroutine(AddDrop());
        }
        else
        {
            animator.SetTrigger(animationTrigger);
            LabManager.Instance.ChargeDropperWithIndicator();
            isCharged = true;
        }
    }
}