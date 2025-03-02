using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RadialCompass : MonoBehaviour
{
    [SerializeField] private RawImage compassImage;
    [SerializeField] private Transform boat;
    
    void Update()
    {
        compassImage.uvRect = new Rect(boat.localEulerAngles.y/360f, 0f, 1f, 1f);
    }
}
