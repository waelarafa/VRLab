using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;
using UnityEngine.Serialization;

public class TubeInteraction : Interaction
{
    public override bool CanInteract { get; set; }

    private string ClosingTheTubeTrigger = "ClosingTheTube";
    private string moveTubeTrigger = "MoveTube";
    private string fillSpectoTubeTrigger = "FillSpectoTube";
    private string fillingTubeTrigger = "FillingTube";

    [SerializeField] private GameObject liquid;
    [SerializeField] private GameObject cover;
    [SerializeField] private Material newSolutionColor;
    [SerializeField] private float waitAnimationTime = 2f;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        AmmoniumExperienceManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }


    public override void Interact()
    {
        if (AmmoniumExperienceManager.Instance.ExperienceState ==
            AmmoniumExperienceState.MovingTheSolutionToTheCardbox)
        {
            animator.SetTrigger(moveTubeTrigger);
            AmmoniumExperienceManager.Instance.MoveTheTube();
        }
        else
        {
            if (AmmoniumExperienceManager.Instance.ExperienceState ==
                AmmoniumExperienceState.FillingTheTube)
            {
                animator.SetTrigger(fillSpectoTubeTrigger);

                StartCoroutine(FillTheTube());

            }
        }
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        UpdateInteractionState();

        if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.ClosingTheTube)
        {
            liquid.SetActive(true);
            animator.SetTrigger(fillingTubeTrigger);
        }

        if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.MovingTheSolutionToTheCardbox)
        {
            cover.SetActive(true);
            animator.SetTrigger(ClosingTheTubeTrigger);
            InteractableObject interactableObject = GetComponent<InteractableObject>();
            interactableObject.ItemActionText = "Move the tube to the cardbox";
        }


        if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.OpeningTheCardbox)
        {
            UpdateSolutionColor();
        }

        if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.FillingTheTube)
        {
            InteractableObject interactableObject = GetComponent<InteractableObject>();
            interactableObject.ItemActionText = "Fill the tube with the solution";
        }
        
    }


    private void UpdateInteractionState()
    {
        CanInteract =
            AmmoniumExperienceManager.Instance.ExperienceState ==
            AmmoniumExperienceState.MovingTheSolutionToTheCardbox ||
            AmmoniumExperienceManager.Instance.ExperienceState ==
            AmmoniumExperienceState.FillingTheTube;
    }


    private void UpdateSolutionColor()
    {
        MeshRenderer meshRenderer = liquid.GetComponent<MeshRenderer>();

        meshRenderer.material = newSolutionColor;
    }
    
    
    private IEnumerator FillTheTube()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        AmmoniumExperienceManager.Instance.FillTheTube();
    }
}