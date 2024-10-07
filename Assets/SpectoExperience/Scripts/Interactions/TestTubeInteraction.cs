using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestTubeInteraction : Interaction
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

        SpectoLabManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        InteractableObject interactableObject = GetComponent<InteractableObject>();
        
        CanInteract = SpectoLabManager.Instance.ExperienceState is SpectoExperienceState.MovingTheSolutionToTheMixer
            or SpectoExperienceState.MovingTheSolutionToTheRack or SpectoExperienceState.FillingTheTube;
        
        switch (SpectoLabManager.Instance.ExperienceState)
        {
            case SpectoExperienceState.AgitatingTheSolution:
                animator.SetTrigger(mixingTrigger);
                break;
            
            case SpectoExperienceState.MovingTheSolutionToTheMixer:
                liquidGameObject.SetActive(true);
                interactableObject.ItemActionText = "Move the solution to the mixer";
                break;
            
            case SpectoExperienceState.MovingTheSolutionToTheRack:
                animator.SetTrigger(finishMixingTrigger);
                interactableObject.ItemActionText = "Move the solution to the rack and leave it resting for 30 minutes";
                break;
            
            case SpectoExperienceState.SettingTheWavelength:
                
                MeshRenderer renderer = liquidGameObject.GetComponent<MeshRenderer>();
                
                renderer.material = SpectoLabManager.Instance.LightBlueLiquidMaterial;
                break;
            
            case SpectoExperienceState.FillingTheTube:

               
                interactableObject.ItemActionText = "Fill the tube with the solution";
                
                break;
           
            case SpectoExperienceState.MovingTheTube:
                liquidGameObject.SetActive(false);
                break;
        }


        
    }


    public override void Interact()
    {
        switch (SpectoLabManager.Instance.ExperienceState)
        {
            case SpectoExperienceState.MovingTheSolutionToTheMixer:
                animator.SetTrigger(moveToMixerTrigger);
                StartCoroutine(MoveTheSolutionToTheMixer());
                
                break;
            case SpectoExperienceState.MovingTheSolutionToTheRack:
                animator.SetTrigger(moveToRackTrigger);
                StartCoroutine(MoveTheSolutionToTheRack());
                break;
            
            case SpectoExperienceState.FillingTheTube:
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
        SpectoLabManager.Instance.MoveSolutionToTheMixer();
    }
    
    private IEnumerator MoveTheSolutionToTheRack()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        SpectoLabManager.Instance.MoveSolutionToTheRack();
    }
    
    private IEnumerator FillTheTubeWithTheSolution()
    {
        yield return new WaitForSeconds(0.8f);
        SpectoLabManager.Instance.FillingTheTube();
    }
    
    
    
    
    
    
}