using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class PlayerSTCircleSync : MonoBehaviour
{
    public static int PosID = Shader.PropertyToID("_PlayerPos");
    public static int SizeID = Shader.PropertyToID("_Size");

    public Material Mat;
    public Camera Cam;
    public LayerMask Mask;

    [SerializeField] [Range(0.00f, 5f)]
    private float Size;

    void Start()
    {
        if (Cam == null)
        {
            try
            {
                Cam = GameObject.Find("MainCamera").GetComponent<Camera>();
            }
            catch
            {
                Debug.LogWarning(
                    $"No camera assigned to script PlayerSTCircleSync! Object script found at {gameObject.name} Failed to find 'MainCamera'. Please attatch a camera to the script"
                );
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        //Getting direction towards camera
        Vector3 dir = Cam.transform.position - transform.position;
        //Create a ray towards the camera
        Ray ray = new Ray(transform.position, dir.normalized);
        //Visaulize Ray
        Debug.DrawRay(transform.position, dir * 3000, Color.red);

        //Cast ray and check 3000 units toward camera, check for objects on the specified layermask
        if (Physics.Raycast(ray, 3000, Mask))
        {
            Mat.SetFloat(SizeID, Size);
        }
        else
            Mat.SetFloat(SizeID, 0);
        Vector3 view = Cam.WorldToViewportPoint(transform.position);
        Mat.SetVector(PosID, view);
    }
}
