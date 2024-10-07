using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Despawner : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(Despawn());
    }

    private IEnumerator Despawn()
    {
        float timeToDespawn = 3f;

        yield return new WaitForSeconds(timeToDespawn);
        
        gameObject.SetActive(false);

    }
    
}
