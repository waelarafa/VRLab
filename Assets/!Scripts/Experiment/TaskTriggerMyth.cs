using UnityEngine;

public class TaskTriggerMyth : MonoBehaviour
{
    public string taskNameToComplete;  // The task associated with this trigger
    private string triggerTag = "Player";  // Default tag for comparison
    private bool isTriggered = false;  // Prevents multiple triggering

    // Method to dynamically set the tag for this trigger
    public void SetTag(string tag)
    {
        triggerTag = tag;  // Set the tag for comparison
    }

    private void OnTriggerEnter(Collider other)
    {

        if (isTriggered) return;  // Prevent double triggering

        if (other.CompareTag(triggerTag))  // Check if the collider matches the tag
        {
            TaskManagerMythOrange taskManager = FindObjectOfType<TaskManagerMythOrange>();
            if (taskManager != null && taskManager.IsCurrentTask(taskNameToComplete))
            {
                taskManager.HandleTaskTrigger(this);  // Notify TaskManager to complete the task
                isTriggered = true;  // Mark as triggered to avoid re-entering
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
