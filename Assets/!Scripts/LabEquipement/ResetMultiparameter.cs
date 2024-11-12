using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResetMultiparameter : MonoBehaviour
{
    [SerializeField] private Transform MultipartameterInitialPos;
 
    private Vector3 initialPosition;
    private Quaternion initialRotation;
    // Start is called before the first frame update
    public void Start()
    {
        initialPosition = MultipartameterInitialPos.position;
        initialRotation = MultipartameterInitialPos.rotation;
    }
    public void resetMutliparameter()
    {
        MultipartameterInitialPos.position = initialPosition;
        MultipartameterInitialPos.rotation = initialRotation;
    }
   
}
