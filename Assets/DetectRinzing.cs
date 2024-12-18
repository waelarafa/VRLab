using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectRinzing : MonoBehaviour
{
    public GameObject bottle;
    // Start is called before the first frame update
    void Update()
    {
        // Get the rotation in local or world space (choose based on your setup)
        float xRotation = NormalizeAngle(bottle.transform.eulerAngles.x);
        float yRotation = NormalizeAngle(bottle.transform.eulerAngles.y);
       
        // Check if the rotation exceeds ±90 degrees
        if (xRotation > 90f || xRotation < -90f || yRotation > 90f || yRotation < -90f)
        {
            Debug.Log("The bottle has rotated beyond ±90°: " + xRotation);
        }
    }

    float NormalizeAngle(float angle)
    {
        // Convert angles greater than 360° or less than 0° to the range [-180, 180]
        while (angle > 180f) angle -= 360f;
        while (angle < -180f) angle += 360f;
        return angle;
    }

   
}
