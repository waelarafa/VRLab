using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TubeCoverInteraction : Interaction
{
    public override bool CanInteract { get; set; }

    private string closeTubeTrigger = "CloseTube";
    
    [SerializeField] private float waitAnimationTime = 1.5f;
    
    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        AmmoniumExperienceManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }


    public override void Interact()
    {
        if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.ClosingTheTube)
        {
            animator.SetTrigger(closeTubeTrigger);
            StartCoroutine(CloseTube());
        }
    }
    
    private IEnumerator CloseTube()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        AmmoniumExperienceManager.Instance.CloseTheTube();
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        UpdateInteractionState();
    }


    private void UpdateInteractionState()
    {
        CanInteract = AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.ClosingTheTube;
    }

    public void DisableTube()
    {
        gameObject.SetActive(false);
    }
    
    
}
