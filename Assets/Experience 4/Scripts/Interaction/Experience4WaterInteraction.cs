using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Experience4WaterInteraction : Interaction
{
    
    [SerializeField] private float waitAnimationTime = 0.75f;
    [SerializeField] private GameObject waterObject;
    [SerializeField] private GameObject highlightObject;

    private string animationTrigger = "PourWater";
    private Animator animator;
    public override bool CanInteract { get; set; }


    private void Start()
    {
        animator = GetComponent<Animator>();

        Experience4Manager.Instance.OnExperienceStateChanged += LabManager_OnExperienceStateChanged;
    }


    private void LabManager_OnExperienceStateChanged(object sender, EventArgs e)
    {
        UpdateInteractionState();
        UpdateWaterContainer();
    }


    private void UpdateInteractionState()
    {

        if (Experience4Manager.Instance.ExperienceState == Experience4State.PouringTheSolution)
        {
            CanInteract = true;
            UpdateWaterColor();
        }
        else
        {
            CanInteract = false;
        }
        
        
    }


    public override void Interact()
    {
        highlightObject.SetActive(false);
        CanInteract = false;
        PlayAnimation();
        StartCoroutine(PourWater());
    }

    private void PlayAnimation()
    {
        animator.SetTrigger(animationTrigger);
    }

    private IEnumerator PourWater()
    {
        yield return new WaitForSeconds(waitAnimationTime);
        Experience4Manager.Instance.PourWater();
    }


    private void UpdateWaterContainer()
    {
        if (Experience4Manager.Instance.ExperienceState != Experience4State.AddingDiazotizationReagent ||
            Experience4Manager.Instance.ExperienceState != Experience4State.PouringTheSolution)
        {
            waterObject.SetActive(false);
        }
    }


    private void UpdateWaterColor()
    {
        
        MeshRenderer meshRenderer = waterObject.GetComponent<MeshRenderer>();

        meshRenderer.materials = ChangeMaterial(meshRenderer, Experience4Manager.Instance.DiazotizatedWaterColor);

    }
    
    private Material[] ChangeMaterial(MeshRenderer renderer, Material material)
    {
        if (renderer.materials.Length > 1)
        {
            Material[] currentMaterials = renderer.materials;
            currentMaterials[1] = material;
            return currentMaterials;
        }

        return null;
    }
    
    
}
