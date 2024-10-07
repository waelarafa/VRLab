using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TaskItem : MonoBehaviour
{
    public TMP_Text taskText; // Text field for the task description
    public Toggle toggle; // The toggle button for task completion

    // Method to set the task description
    public void SetTaskDescription(string description)
    {
        taskText.text = description;
    }

    // Method to mark the task as completed
    public void CompleteTask()
    {
        toggle.isOn = true; // Mark the task as completed
        toggle.interactable = false; // Disable user interaction
    }

    // Method to set the color of the task text
    public void SetTaskTextColor(Color color)
    {
        taskText.color = color; // Apply the color to the text
    }
}
