using BNG;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandUIManager : MonoBehaviour
{
    public GameObject TaskCanva;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
