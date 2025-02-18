using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField, Tooltip("This is the reference that the camera always will be pointed at")] private GameObject cameraReference;
    [SerializeField, Range(0, 1f)] private float cameraSmoothing;
    [SerializeField, Tooltip("Camera position relative to player")] private Vector3 cameraPos;

    void FixedUpdate()
    {
        Vector3 targetPosition = cameraReference.transform.position - cameraReference.transform.forward * cameraPos.z;
        targetPosition.y = cameraPos.y;
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSmoothing);
        transform.LookAt(cameraReference.transform);
    }
}
