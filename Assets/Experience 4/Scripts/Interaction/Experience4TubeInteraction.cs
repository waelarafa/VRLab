using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience4TubeInteraction : Interaction
{
    public override bool CanInteract { get; set; }
    
    [SerializeField] private float waitAnimationTime = 2f;
    [SerializeField] private GameObject filledGameObject;

    private string trigger = "MoveSpectoTube";

    private Animator animator;

    private void Start()
    {
        animator = GetComponent<Animator>();

        Experience4Manager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }

    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        CanInteract = Experience4Manager.Instance.ExperienceState == Experience4State.MovingTheTube;

        if (Experience4Manager.Instance.ExperienceState == Experience4State.MovingTheTube)
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
        Experience4Manager.Instance.MovingTheTube();
    }
}
