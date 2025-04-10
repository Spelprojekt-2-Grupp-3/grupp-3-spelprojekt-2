using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class InventoryMenu : MonoBehaviour
{
    //[SerializeField] private InventoryController inventoryController;
    [SerializeField] private QuestLog questLog;
    [SerializeField] private List<GameObject> tabObjects;

    private PlayerInputActions playerInput;
    private InputAction openJournal;
    private InputAction tabRight;
    private InputAction tabLeft;

    private float scaleIncrease;
    
    private int selectedMenuIndex;

    [SerializeField] private GameObject menuObject;

    private bool isOpen;
    private void Awake()
    {
        playerInput = new PlayerInputActions();
    }

    private void OnEnable()
    {
        openJournal = playerInput.Journal.Journal;
        tabRight = playerInput.UI.MenuRight;
        tabLeft = playerInput.UI.MenuLeft;
        EnableControls();
        
        Events.startPlayer.AddListener(EnableControls);
        Events.stopPlayer.AddListener(ForceClose);
    }

    private void OnDisable()
    {
        DisableControls();
        
        Events.startPlayer.RemoveListener(EnableControls);
        Events.stopPlayer.RemoveListener(ForceClose);
    }

    private void Start()
    {
        DisableOther();
        menuObject.SetActive(false);
    }

    void DisableControls()
    {
        DisableOther();
        openJournal.Disable();
    }

    void ForceClose()
    {
        DisableControls();
        isOpen = false;
        menuObject.SetActive(false);
    }
    
    void EnableControls()
    {
        EnableOther();
        openJournal.Enable();
    }

    void EnableOther()
    {
        tabLeft.Enable();
        tabRight.Enable();
    }

    void DisableOther()
    {
        tabLeft.Disable();
        tabRight.Disable();
    }
    
    private void Update()
    {
        if (openJournal.WasPressedThisFrame())
        {
            isOpen = !isOpen;
            menuObject.SetActive(isOpen);
            //if(isOpen)
            //    EnableOther();
            //else 
            //    DisableOther();
        }

        //if (isOpen)
        //{
        //    if (tabRight.WasPressedThisFrame())
        //    {
        //        selectedMenuIndex++;
        //        if (selectedMenuIndex > 1)
        //            selectedMenuIndex = 0;
        //    }
        //    else if (tabLeft.WasPressedThisFrame())
        //    {
        //        selectedMenuIndex--;
        //        if (selectedMenuIndex < 0)
        //            selectedMenuIndex = 1;
        //    }
        //    
        //    switch (selectedMenuIndex)
        //    {
        //        case 0:
        //            inventoryController.SetEnableState(true);
        //            SelectTab(tabObjects[0]);
        //            questLog.SetEnableState(false);
        //            break;
        //        case 1:
        //            inventoryController.SetEnableState(false);
        //            questLog.SetEnableState(true);
        //            break;
        //    }
        //}
        //else
        //{
        //    inventoryController.SetEnableState(false);
        //    questLog.SetEnableState(false);
        //}
    }

    void SelectTab(GameObject tab)
    {
        foreach (var flik in tabObjects)
        {
            if (flik == tab)
            {
                tab.transform.localScale *= scaleIncrease;
            }
            else
            {
                tab.transform.localScale = new Vector3(1,1,1);
            }
        }
    }
    
}
