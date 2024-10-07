using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience4TestTubeInteraction : Interaction
{
    public override bool CanInteract { get; set; }

    [SerializeField] private GameObject liquidGameObject;

    [SerializeField] private float waitAnimationTime = 2f;

    private string moveToMixerTrigger = "MoveToMixer";
    private string moveToRackTrigger = "MoveToRack";
    private string mixingTrigger = "Mixing";
    private string finishMixingTrigger = "FinishMixing";
    private string fillingTheTubeTrigger = "FillingTheTube";

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        Experience4Manager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        InteractableObject interactableObject = GetComponent<InteractableObject>();

        CanInteract = Experience4Manager.Instance.ExperienceState is Experience4State.MovingTheSolutionToTheMixer
            or Experience4State.MovingTheSolutionToTheRack or Experience4State.FillingTheTube;

        switch (Experience4Manager.Instance.ExperienceState)
        {
            case Experience4State.AgitatingTheSolution:
                animator.SetTrigger(mixingTrigger);
                break;

            case Experience4State.MovingTheSolutionToTheMixer:
                liquidGameObject.SetActive(true);
                interactableObject.ItemActionText = "Move the solution to the mixer";
                break;

            case Experience4State.MovingTheSolutionToTheRack:
                animator.SetTrigger(finishMixingTrigger);
                interactableObject.ItemActionText = "Move the solution to the rack and leave it resting for 30 minutes";
                break;

            case Experience4State.SettingTheWavelength:

                MeshRenderer renderer = liquidGameObject.GetComponent<MeshRenderer>();

                renderer.material = Experience4Manager.Instance.DiazotizatedSolutionColor;
                break;

            case Experience4State.FillingTheTube:


                interactableObject.ItemActionText = "Fill the tube with the solution";

                break;

            case Experience4State.MovingTheTube:
                liquidGameObject.SetActive(false);
                break;
        }
    }


    public override void Interact()
    {
        switch (Experience4Manager.Instance.ExperienceState)
        {
            case Experience4State.MovingTheSolutionToTheMixer:
                animator.SetTrigger(moveToMixerTrigger);
                StartCoroutine(MoveTheSolutionToTheMixer());

                break;
            case Experience4State.MovingTheSolutionToTheRack:
                animator.SetTrigger(moveToRackTrigger);
                StartCoroutine(MoveTheSolutionToTheRack());
                break;

            case Experience4State.FillingTheTube:
                animator.SetTrigger(fillingTheTubeTrigger);
                StartCoroutine(FillTheTubeWithTheSolution());
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }


    private IEnumerator MoveTheSolutionToTheMixer()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        Experience4Manager.Instance.MoveSolutionToTheMixer();
    }

    private IEnumerator MoveTheSolutionToTheRack()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        Experience4Manager.Instance.MoveSolutionToTheRack();
    }

    private IEnumerator FillTheTubeWithTheSolution()
    {
        yield return new WaitForSeconds(0.8f);
        Experience4Manager.Instance.FillingTheTube();
    }
}