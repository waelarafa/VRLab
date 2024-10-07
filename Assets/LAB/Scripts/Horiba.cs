using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Horiba : MonoBehaviour
{
    public GameObject horiba;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            horiba.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            horiba.SetActive(false);
        }
    }
}
