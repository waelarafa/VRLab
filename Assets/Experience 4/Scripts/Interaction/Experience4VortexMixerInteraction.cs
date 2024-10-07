using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience4VortexMixerInteraction : Interaction
{
    public override bool CanInteract { get; set; }
    
    
    private void Start()
    {
        Experience4Manager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        CanInteract = Experience4Manager.Instance.ExperienceState == Experience4State.TurningOnTheMixer;
    }
    
    public override void Interact()
    {
        Experience4Manager.Instance.AgitateTheSolution();
    }
}
