using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField, Range(0, 1f)] private float cameraSmoothing;
    [SerializeField, Tooltip("Camera position relative to player")] private Vector3 cameraPos;
    private Rigidbody rb;

    private void Start()
    {
        rb = player.GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        Vector3 targetPosition = player.transform.position + cameraPos;
        transform.position = Vector3.Lerp(transform.position, targetPosition, cameraSmoothing);
        transform.LookAt(player.transform);
    }
}
