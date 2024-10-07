using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoniumSpectoPhotometerCover : Interaction
{
    public override bool CanInteract { get; set; }
    
    [SerializeField] private float waitAnimationTime = 2f;
    
    private string openTrigger = "OpenSpecto";
    private string closeTrigger = "CloseSpecto";
    
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        CanInteract = AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.OpeningTheSpectoPhotometer;
        AmmoniumExperienceManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        InteractableObject interactableObject = GetComponent<InteractableObject>();
        
        CanInteract = AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.OpeningTheSpectoPhotometer || 
                      AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.ClosingTheSpectoPhotometer;
        
        
        if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.ClosingTheSpectoPhotometer)
        {
            interactableObject.ItemActionText = "Close the spectophotometer cover";
        }
    }
    
    private IEnumerator OpenPhotometer()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        AmmoniumExperienceManager.Instance.OpenTheSpectoPhotometer();
    }
    
    private IEnumerator ClosePhotometer()
    {
        CanInteract = false; 
        yield return new WaitForSeconds(waitAnimationTime);
        AmmoniumExperienceManager.Instance.CloseTheSpectoPhotometer();
    }
    
    
    public override void Interact()
    {
        if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.OpeningTheSpectoPhotometer)
        {
            animator.SetTrigger(openTrigger);
            StartCoroutine(OpenPhotometer());
        }
        else
        if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.ClosingTheSpectoPhotometer)
        {
            animator.SetTrigger(closeTrigger);
            StartCoroutine(ClosePhotometer());
        }
        
       
    }
}
