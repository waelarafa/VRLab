using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using UnityEngine;

public class InteractableObject : MonoBehaviour
{
    [SerializeField] private string itemActionText;
    [SerializeField] private GameObject highlightObject;
    [SerializeField] private Interaction interaction;

    private bool isMouseOver;


    private void OnMouseOver()
    {
        if (LabManager.Instance != null)
        {
            if (LabManager.Instance.IsGamePaused)
                return;
        }
        else
        {
            if (Experience4Manager.Instance != null)
            {
                if (Experience4Manager.Instance.IsGamePaused)
                    return;
            }
            else
            {
                if (SpectoLabManager.Instance != null)
                {
                    if (SpectoLabManager.Instance.IsGamePaused)
                        return;
                }
                else
                {
                    if (AmmoniumExperienceManager.Instance != null)
                    {
                        if (AmmoniumExperienceManager.Instance.IsGamePaused)
                            return;
                    }
                }
            }
        }


        if (!isMouseOver)
        {
            if (interaction.CanInteract)
            {
                highlightObject.SetActive(true);
                if (LabUIManager.Instance != null)
                    LabUIManager.Instance.SetActionText(itemActionText);
                else if (SpectoUIManager.Instance != null)
                    SpectoUIManager.Instance.SetActionText(itemActionText);
                else if (Experience4UIManager.Instance != null)
                    Experience4UIManager.Instance.SetActionText(itemActionText);
                else if (AmmoniumExperienceUIManager.Instance != null)
                    AmmoniumExperienceUIManager.Instance.SetActionText(itemActionText);
            }
            else
            {
                highlightObject.SetActive(false);
                if (LabUIManager.Instance != null)
                    LabUIManager.Instance.ClearActionText();
                else if (SpectoUIManager.Instance != null)
                    SpectoUIManager.Instance.ClearActionText();
                else if (Experience4UIManager.Instance != null)
                    Experience4UIManager.Instance.ClearActionText();
                else if (AmmoniumExperienceUIManager.Instance != null)
                    AmmoniumExperienceUIManager.Instance.ClearActionText();
            }
        }

        isMouseOver = true;
    }

    private void OnMouseExit()
    {
        highlightObject.SetActive(false);
        if (LabUIManager.Instance != null)
            LabUIManager.Instance.ClearActionText();
        else if (SpectoUIManager.Instance != null)
            SpectoUIManager.Instance.ClearActionText();
        else if (Experience4UIManager.Instance != null)
            Experience4UIManager.Instance.ClearActionText();
        else if (AmmoniumExperienceUIManager.Instance != null)
            AmmoniumExperienceUIManager.Instance.ClearActionText();
        isMouseOver = false;
    }


    private void OnMouseDown()
    {
        if (LabManager.Instance != null)
        {
            if (LabManager.Instance.IsGamePaused)
                return;
        }
        else
        {
            if (Experience4Manager.Instance != null)
            {
                if (Experience4Manager.Instance.IsGamePaused)
                    return;
            }
            else
            {
                if (SpectoLabManager.Instance != null)
                {
                    if (SpectoLabManager.Instance.IsGamePaused)
                        return;
                }
                else
                {
                    if (AmmoniumExperienceManager.Instance != null)
                    {
                        if (AmmoniumExperienceManager.Instance.IsGamePaused)
                            return;
                    }
                }
            }
        }


        if (interaction.CanInteract)
        {
            interaction.Interact();
        }
    }

    public string ItemActionText
    {
        get => itemActionText;
        set => itemActionText = value;
    }
}