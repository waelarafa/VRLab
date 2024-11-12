using UnityEngine;

public class TaskTriggerNitrV2 : MonoBehaviour
{
    public string taskNameToComplete;       // The task associated with this trigger
    private string triggerTag = "Player";   // Default tag for comparison
    private bool isTriggered = false;       // Used to prevent unintended retriggers if needed

    public int requiredTriggerCount = 1;    // Number of times the trigger needs to be activated
    private int currentTriggerCount = 0;    // Tracks current activation count

    // Method to dynamically set the tag for this trigger
    public void SetTag(string tag)
    {
        triggerTag = tag;  // Set the tag for comparison
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag(triggerTag))  // Check if the collider matches the tag
        {
            TaskManagerNitrV2 taskManager = FindObjectOfType<TaskManagerNitrV2>();

            if (taskManager != null && taskManager.IsCurrentTask(taskNameToComplete))
            {
                currentTriggerCount++;  // Increment the trigger count

                if (currentTriggerCount >= requiredTriggerCount)
                {
                    taskManager.HandleTaskTrigger(this);  // Notify TaskManager to complete the task
                    isTriggered = true;  // Mark as triggered to avoid further activations if required
                }
                else
                {
                    Debug.Log($"Trigger count: {currentTriggerCount}/{requiredTriggerCount}");
                }
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
        currentTriggerCount = 0;
        isTriggered = false;
    }
}
