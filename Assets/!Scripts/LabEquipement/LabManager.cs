using UnityEngine;
using UnityEngine.UI;
using TMPro;
using BNG;
using System.Collections;

public class TaskManager : MonoBehaviour
{
    public GameObject taskPrefab; // Assign the Task prefab
    public Transform taskPanel; // Assign the Panel where tasks will be instantiated
    public TaskList taskList; // Reference to the TaskList ScriptableObject
    public bool checkFirstItem;
    public Color taskTextColor; // Exposed color for task text
    private int tasksLength;
    private float progressRate;
    private float TotalProgress;
    public TextMeshProUGUI progressUI;
    public GameObject TaskCanva;
    public GameObject endCanva;

    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip[] audioClips; // Array to hold audio clips
    [SerializeField] private AudioClip finalAudioClip; // Final audio clip for completion
    private AudioClip currentClip; // Variable to store the current playing audio clip

    private void Start()
    {
        endCanva.SetActive(false);

        tasksLength = taskList.tasks.Length;
        progressRate = 100 / tasksLength;
        TotalProgress = 0;
        for (int i = 0; i < tasksLength; i++)
        {
            bool isChecked = (i == 0 && checkFirstItem); // Only check the first task
            CreateTask(taskList.tasks[i].taskName, taskList.tasks[i].taskDescription, isChecked);
        }
    }

    void CreateTask(string taskName, string taskDescription, bool isChecked)
    {
        GameObject taskObj = Instantiate(taskPrefab, taskPanel);
        taskObj.name = taskName;

        TaskItem taskItem = taskObj.GetComponent<TaskItem>();
        taskItem.SetTaskDescription(taskDescription);
        taskItem.SetTaskTextColor(taskTextColor);
        taskItem.toggle.isOn = isChecked;
        taskItem.toggle.interactable = false;

        taskItem.toggle.onValueChanged.AddListener(delegate { OnTaskToggle(taskItem.toggle); });
    }

    void OnTaskToggle(Toggle toggle)
    {
        // Logic when a task toggle is changed
    }

    public void CompleteTaskByName(string taskName)
    {
        foreach (Transform child in taskPanel)
        {
            if (child.gameObject.name == taskName)
            {
                TaskItem taskItem = child.GetComponent<TaskItem>();
                if (taskItem != null && !taskItem.toggle.isOn)
                {
                    upgradeProgress();
                    taskItem.CompleteTask(); // Mark task as completed

                    // Play the audio corresponding to the task
                    PlayTaskAudio(taskName);
                    Debug.Log(taskName + " is completed.");
                }
                return;
            }
        }

        Debug.Log("Task not found: " + taskName);
    }

    public void upgradeProgress()
    {
        TotalProgress += progressRate;
        progressUI.text = "Progress " + TotalProgress + "%";
        Debug.Log("TotalProgress: " + TotalProgress);
        if (TotalProgress >= 100) // Use >= to handle overshooting due to floating point errors
        {
            endCanva.SetActive(true);

            StartCoroutine(PlayFinalAudio());

            Debug.Log("End Canvas enabled.");
        }
    }

    // Play audio corresponding to the completed task
    private void PlayTaskAudio(string taskName)
    {
        // Stop the currently playing audio
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }

        // Find the corresponding audio clip
        AudioClip clipToPlay = FindAudioClipByTaskName(taskName);
        if (clipToPlay != null)
        {
            audioSource.clip = clipToPlay;
            audioSource.Play();
            currentClip = clipToPlay; // Store the current clip
        }
        else
        {
            Debug.LogWarning("Audio clip not found for task: " + taskName);
        }
    }

    // Find the audio clip based on the task name
    private AudioClip FindAudioClipByTaskName(string taskName)
    {
        // Assuming task names match the audio clip names
        foreach (AudioClip clip in audioClips)
        {
            if (clip.name == taskName)
            {
                return clip; // Return the clip if the name matches
            }
        }
        return null; // Return null if no matching clip is found
    }

    // Play the final audio when all tasks are completed
    private IEnumerator PlayFinalAudio()
    {
        yield return new WaitForSeconds(3f);   

        audioSource.Stop(); 
        audioSource.clip = finalAudioClip;
        audioSource.Play();
    }

    public void Update()
    {
        if (InputBridge.Instance.XButtonDown)
        {
            TaskCanva.SetActive(true);
        }
        if (InputBridge.Instance.XButtonUp)
        {
            TaskCanva.SetActive(false);
        }
    }
}
