using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoxInteraction : Interaction
{
    public override bool CanInteract { get; set; }

    private string closeBoxTrigger = "CloseBox";
    private string openBoxTrigger = "OpenBox";

    [SerializeField] private GameObject coverHighlight;

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();
        AmmoniumExperienceManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }


    public override void Interact()
    {
        if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.ClosingTheCardBox)
        {
            animator.SetTrigger(closeBoxTrigger);
            AmmoniumExperienceManager.Instance.CloseTheCardBox();
        }
        else
        {
            if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.OpeningTheCardbox)
            {
                animator.SetTrigger(openBoxTrigger);
                AmmoniumExperienceManager.Instance.OpenTheCardBox();
            }
        }
        
        
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        UpdateInteractionState();
    }


    private void UpdateInteractionState()
    {
        CanInteract = AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.ClosingTheCardBox ||
                      AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.OpeningTheCardbox;
        
        if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.OpeningTheCardbox)
        {
            coverHighlight.SetActive(true);
        }
        
    }
}