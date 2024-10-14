using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passnext : MonoBehaviour
{
    public TaskManager2 taskmanage;
    // Start is called before the first frame update
    public void passnextt()
    {
        taskmanage.CompleteCurrentTask();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
