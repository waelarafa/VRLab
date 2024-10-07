using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhenolNitroprusside : Interaction
{
   
    [SerializeField] private float waitAnimationTime = 1f;

    private Animator animator;
    
    private string animationTrigger = "AddReagent";

    public override bool CanInteract { get; set; }

    private void Start()
    {
        animator = GetComponent<Animator>();
        AmmoniumExperienceManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }


    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        UpdateInteractionState();
    }

    private void UpdateInteractionState()
    {
        CanInteract = AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.AddingPhenolNitroprusside;
    }


    public override void Interact()
    {
        CanInteract = false;
        PlayAnimation();
        StartCoroutine(AddReagent());
    }

    private void PlayAnimation()
    {
        animator.SetTrigger(animationTrigger);
    }

    private IEnumerator AddReagent()
    {
        yield return new WaitForSeconds(1f);
        AmmoniumExperienceManager.Instance.AddPhenolNitroprusside();
    }
    
    
}
