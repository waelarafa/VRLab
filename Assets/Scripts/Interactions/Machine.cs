using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Machine : MonoBehaviour
{
    [SerializeField] private GameObject dropObject;


    private string addDropAnimationTrigger = "AddHCIDrop";
    private string fillSyringeAnimationTrigger = "FillSyringe";
    private string idleSyringeAnimationTrigger = "IdleSyringe";
    
    private Animator animator;

   


    private void Start()
    {
        LabManager.Instance.OnHCIAdded += LabManager_OnHCIAdded;
        LabManager.Instance.OnHCIDropAdded += LabManager_OnHCIDropAdded;
        LabManager.Instance.OnExperienceChanged += LabManager_OnExperienceChanged;
        
        animator = GetComponent<Animator>();
    }

    private void LabManager_OnExperienceChanged(object sender, EventArgs e)
    {
        animator.SetTrigger(idleSyringeAnimationTrigger);
        animator.SetBool(fillSyringeAnimationTrigger,false);
        dropObject.SetActive(false);
    }

    private void LabManager_OnHCIDropAdded(object sender, EventArgs e)
    {
        animator.SetTrigger(addDropAnimationTrigger);
    }

    private void LabManager_OnHCIAdded(object sender, EventArgs e)
    {
        animator.SetTrigger(fillSyringeAnimationTrigger);
    }
}
