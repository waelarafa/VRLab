using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldCanvas : MonoBehaviour
{
    private new Transform camera;
    private void Start()
    {
        camera = Camera.main.transform;
    }
    private void LateUpdate()
    {
        transform.forward = transform.position - camera.position;
    }
}
