using System.Collections.Generic;
using UnityEngine;

public class TaskTriggerNitrV2 : MonoBehaviour
{
    public string taskNameToComplete;  // The task associated with this trigger
    private string triggerTag = "Player";  // Default tag for comparison
    private bool isTriggered = false;  // Prevents multiple triggering
    private List<string> childTasks;
    private int currentChildTaskIndex = 0;

    // Method to dynamically set the tag for this trigger
    public void SetTag(string tag)
    {
        triggerTag = tag;  // Set the tag for comparison
    }
    public void DisaBleTrigger()
    {
        isTriggered = true;
    }
    public void SetChildTasks(List<string> tasks)
    {
        childTasks = tasks;
        currentChildTaskIndex = 0;
    }
    public int GetChildTaskIndex()
    {
      
        return currentChildTaskIndex;  // All child tasks are complete
    }
    public void NextChildTask()
    {
        currentChildTaskIndex++;
    }
    public bool HasMoreChildTasks()
    {
        return currentChildTaskIndex < childTasks.Count;
    }
    private void OnTriggerEnter(Collider other)
    {

        if (isTriggered) return;  // Prevent double triggering

        if (other.CompareTag(triggerTag))  // Check if the collider matches the tag
        {
            TaskManagerNitrV2 taskManager = FindObjectOfType<TaskManagerNitrV2>();
            if (taskManager != null && taskManager.IsCurrentTask(taskNameToComplete))
            {
                Debug.Log("the child of index got triggred"+ currentChildTaskIndex);
                taskManager.HandleTaskTrigger(this);  // Notify TaskManager to complete the task
                //isTriggered = true;  // Mark as triggered to avoid re-entering
            }
            else
            {
                Debug.LogWarning("This task is not the current task.");
            }
        }
    }

    // Resets the trigger for testing or re-using the trigger later
    public void ResetTrigger()
    {
        isTriggered = false;
    }
}
