using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Compass : MonoBehaviour
{
    public GameObject Goal;

    RectTransform rt;

    void Start()
    {
        rt = GetComponent<RectTransform>();
    }

    void Update()
    {
        Vector3 objScreenPos = Camera.main.WorldToScreenPoint(Goal.transform.position);
        
        Vector3 dir = (objScreenPos - rt.position).normalized;
        
        float angle = Mathf.Rad2Deg * Mathf.Acos(Vector3.Dot(dir, Vector3.up));
        
        Vector3 cross = Vector3.Cross(dir, Vector3.up);
        angle = -Mathf.Sign(cross.z) * angle;
        
        rt.localEulerAngles = new Vector3(rt.localEulerAngles.x, rt.localEulerAngles.y, angle);
    }

    void switchQuest(GameObject currentQuest)
    {
        Goal = currentQuest;
    }
}
