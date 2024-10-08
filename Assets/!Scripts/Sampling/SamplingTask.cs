using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "NewSamplingList", menuName = "Sampling List")]

public class SamplingTasks : MonoBehaviour
{
    [System.Serializable]
    public class samplingTasks
    {
        public string taskName; // The name of the task
        [TextArea]
        public string taskDescription; // The description of the task
    }

    public samplingTasks[] tasks; // Array to hold multiple tasks
}
