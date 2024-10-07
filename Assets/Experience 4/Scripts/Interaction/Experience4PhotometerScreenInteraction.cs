using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience4PhotometerScreenInteraction : Interaction
{
    public override bool CanInteract { get; set; }


    private void Start()
    {
        Experience4Manager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        CanInteract = Experience4Manager.Instance.ExperienceState is Experience4State.SettingTheWavelength;
    }


    public override void Interact()
    {
        CanInteract = false;
        Experience4Manager.Instance.SettingTheWavelength();
    }
}