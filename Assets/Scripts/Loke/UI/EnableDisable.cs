using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.UI;
using UnityEngine;

public class EnableDisable : MonoBehaviour
{
    public GameObject gameObject;

    private PlayerInputActions playerInputActions;

    public void switchState()
    {
        if (gameObject.activeInHierarchy)
        {
            gameObject.SetActive(false);
            playerInputActions.UI.Disable();
            playerInputActions.Player.Enable();
        }
        else
        {
            gameObject.SetActive(true);
            playerInputActions.Player.Disable();
            playerInputActions.UI.Enable();
        }
    }
}
