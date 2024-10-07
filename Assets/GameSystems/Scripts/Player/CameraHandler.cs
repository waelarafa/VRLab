using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraHandler : MonoBehaviour
{
    [SerializeField] private float sensitivity = 450;
    [SerializeField] private Transform player;

    float rotationUpDown;

    private void LateUpdate()
    {
        RotationHandler();
    }
    private void RotationHandler()
    {
        Vector2 mouseInput = InputHandler.instance.mouseInput * sensitivity * Time.deltaTime;

        rotationUpDown -= mouseInput.y;
        rotationUpDown = Mathf.Clamp(rotationUpDown, -90, 90);
        transform.localRotation = Quaternion.Euler(rotationUpDown, 0, 0);

        player.Rotate(Vector3.up * mouseInput.x);
    }
}
