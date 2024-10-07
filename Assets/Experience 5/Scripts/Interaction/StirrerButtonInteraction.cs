using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StirrerButtonInteraction : Interaction
{
  
    public override bool CanInteract { get; set; }
    
    private void Start()
    {
        AmmoniumExperienceManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }


    
    public override void Interact()
    {
       AmmoniumExperienceManager.Instance.TurnOnTheStirrer();
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        UpdateInteractionState();
    }


    private void UpdateInteractionState()
    {
        CanInteract = AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.TurningOnTheStirrer;
    }
    
}
