using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VortexMixerInteraction : Interaction
{
    public override bool CanInteract { get; set; }
    
    
    private void Start()
    {
        SpectoLabManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        CanInteract = SpectoLabManager.Instance.ExperienceState == SpectoExperienceState.TurningOnTheMixer;
    }
    
    public override void Interact()
    {
        SpectoLabManager.Instance.AgitateTheSolution();
    }
}
