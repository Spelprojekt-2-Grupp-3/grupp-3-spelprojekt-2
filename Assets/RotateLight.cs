using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RotateLight : MonoBehaviour
{
    [SerializeField] private float rotationsPerSecond;

    void Update()
    {
        gameObject.transform.Rotate(new Vector3(0f, rotationsPerSecond * Time.deltaTime * 360, 0f));
    }
}
