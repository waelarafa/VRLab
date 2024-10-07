using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoniumSpectophotometerScreenInteraction : Interaction
{
    public override bool CanInteract { get; set; }


    private void Start()
    {
        AmmoniumExperienceManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        CanInteract =  AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.SettingTheWavelength;
    }
    

    public override void Interact()
    {
        CanInteract = false;
        AmmoniumExperienceManager.Instance.SetTheWavelength();
    }
}
