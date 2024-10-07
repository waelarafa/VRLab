using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlkalineSolutionInteraction : Interaction
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

        isCharged = true;
        
        CanInteract = AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.AddingAlkalineSolution;
       
        AmmoniumExperienceManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
       
    }
    
    
    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        CanInteract = AmmoniumExperienceManager.Instance.ExperienceState ==  AmmoniumExperienceState.AddingAlkalineSolution;
    }
    
    private IEnumerator AddDrop()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        AmmoniumExperienceManager.Instance.AddAlkalineSolution();
    }


    public override void Interact()
    {
        if (isCharged)
        {
            animator.SetTrigger(addDropTrigger);
            StartCoroutine(AddDrop());
        }
    }

}
