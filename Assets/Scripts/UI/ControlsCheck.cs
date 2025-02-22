using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class ControlsCheck : MonoBehaviour
{
    [SerializeField] private GameObject moveControls;
    [SerializeField] private GameObject interactControls;
    [SerializeField] private GameObject gasControls;
    [SerializeField] private CurrentInputIcons inputIcons;

    private void Awake()
    {
        UpdateIcons();
    }

    private void OnEnable()
    {
        Events.updateIcons.AddListener(UpdateIcons);
    }

    private void UpdateIcons()
    {
        moveControls.GetComponent<Image>().sprite = inputIcons.currentInputDevice.moveSprite;
        interactControls.GetComponent<Image>().sprite = inputIcons.currentInputDevice.interactSprite;
        gasControls.GetComponent<Image>().sprite = inputIcons.currentInputDevice.gasSprite;
    }
}
