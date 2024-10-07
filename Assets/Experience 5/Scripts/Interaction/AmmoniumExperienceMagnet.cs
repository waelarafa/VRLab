using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoniumExperienceMagnet : Interaction
{

    public override bool CanInteract { get; set; }
    
    [SerializeField] private float waitAnimationTime = 1f;
    private string animationTrigger = "MoveMagnetToBeaker";
    
    private Animator animator;

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
        CanInteract = AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.AddingTheMagneticBar; 
    }
    
    public override void Interact()
    {
        animator.SetTrigger(animationTrigger);
        StartCoroutine(AddMagnet());
    }
    
    
    private IEnumerator AddMagnet()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        AmmoniumExperienceManager.Instance.AddMagneticBar();
        yield return new WaitForSeconds(0.5f);
        gameObject.SetActive(false);
    }
    
    
}
