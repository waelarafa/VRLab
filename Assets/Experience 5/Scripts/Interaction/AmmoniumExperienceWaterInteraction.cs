using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoniumExperienceWaterInteraction : Interaction
{
    [SerializeField] private float waitAnimationTime = 0.75f;
    [SerializeField] private GameObject waterObject;
    [SerializeField] private GameObject highlightObject;
    [SerializeField] private GameObject magnetObject;

    private string animationTrigger = "MoveWaterBeakerToStirrer";
    private string mixingAnimationTrigger = "StirTheWater";
    private string IdleAnimationTrigger = "StopStirring";
    private string pourWaterInTubeTrigger = "PourWaterInTube";
    private Animator animator;
    public override bool CanInteract { get; set; }


    private void Start()
    {
        animator = GetComponent<Animator>();

        AmmoniumExperienceManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }


    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        UpdateInteractionState();
         UpdateWaterContainer();
    }


    private void UpdateInteractionState()
    {
        if (AmmoniumExperienceManager.Instance.ExperienceState ==
            AmmoniumExperienceState.MovingTheSolutionToTheStirrer ||
            AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.PouringTheSolutionIntoTheTube)
        {
            CanInteract = true;

            if (AmmoniumExperienceManager.Instance.ExperienceState ==
                AmmoniumExperienceState.MovingTheSolutionToTheStirrer)
            {
                magnetObject.SetActive(true);
            }
        }
        else
        {
            CanInteract = false;
        }
        
        
        if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.MixingTheSolution)
        {
            animator.SetTrigger(mixingAnimationTrigger);
        }
        
        if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.PouringTheSolutionIntoTheTube)
        {
            animator.SetTrigger(IdleAnimationTrigger);
            InteractableObject interactableObject = GetComponent<InteractableObject>();
            interactableObject.ItemActionText = "Pour the solution into the tube.";

        }
    }


    public override void Interact()
    {
        highlightObject.SetActive(false);
        CanInteract = false;
        if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.PouringTheSolutionIntoTheTube)
        {
            PlayPouringAnimation();
            AmmoniumExperienceManager.Instance.PourWaterInTube();
        }
        else
        {
            PlayAnimation();
            StartCoroutine(MoveWaterBeakerToStirrer());   
        }
        
       
    }

    private void PlayAnimation()
    {
        animator.SetTrigger(animationTrigger);
    }
    
    private void PlayPouringAnimation()
    {
        animator.SetTrigger(pourWaterInTubeTrigger);
    }
    
    

    private IEnumerator MoveWaterBeakerToStirrer()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        AmmoniumExperienceManager.Instance.MoveWaterBeakerToStirrer();
    }


    private void UpdateWaterContainer()
    {
        if((AmmoniumExperienceManager.Instance.ExperienceState !=
            AmmoniumExperienceState.MovingTheSolutionToTheStirrer ||
            AmmoniumExperienceManager.Instance.ExperienceState != AmmoniumExperienceState.PouringTheSolutionIntoTheTube || 
            AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.MixingTheSolution))
        {
            waterObject.SetActive(false);
        }
    }
}