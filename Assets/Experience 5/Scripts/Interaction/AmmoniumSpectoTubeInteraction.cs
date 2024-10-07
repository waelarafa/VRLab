using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AmmoniumSpectoTubeInteraction : Interaction
{
    public override bool CanInteract { get; set; }
    
    [SerializeField] private float waitAnimationTime = 2f;
    [SerializeField] private GameObject filledGameObject;

    private string trigger = "MoveSpectoTube";

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        AmmoniumExperienceManager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        CanInteract = AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.MovingTheTube;

        if (AmmoniumExperienceManager.Instance.ExperienceState == AmmoniumExperienceState.MovingTheTube)
        {
            filledGameObject.SetActive(true);
        }
    }

    public override void Interact()
    {
        animator.SetTrigger(trigger);
        StartCoroutine(MoveTube());
    }

    private IEnumerator MoveTube()
    {
        yield return new WaitForSeconds(waitAnimationTime - 0.4f);
        AmmoniumExperienceManager.Instance.MoveTheSpectoTube();
    }
}
