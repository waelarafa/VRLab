using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class SpectoPhotometerScreenInteraction : Interaction
{
    public override bool CanInteract { get; set; }


    private void Start()
    {
        SpectoLabManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        CanInteract = SpectoLabManager.Instance.ExperienceState is SpectoExperienceState.SettingTheWavelength;
    }
    

    public override void Interact()
    {
        CanInteract = false;
        SpectoLabManager.Instance.SettingTheWavelength();
    }
}