using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class MouseManager : MonoBehaviour
{
    private PlayerInputActions _playerController;
    private Camera _mainCamera;
    
    [SerializeField]
    private bool showRaysOnClick;
    [SerializeField]
    private float rayLength = 20f;

    void Awake()
    {
        _playerController = new PlayerInputActions();
        _mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0].GetComponent<Camera>();
        
        _playerController.Player.Interact.performed += _ => PerformedClick();

    }

    //Finns en risk att man kan clicka på objekt bakom UI element
    private void PerformedClick()
    {
        DetectObjectOnClick();
    }
    
    private void OnEnable()
    {
        _playerController.Enable();
    }

    private void OnDisable()
    {
        _playerController.Disable();
    }

    //Skjuter en ray från kameran till scenen, och kollar om rayen intersectar objekt
    
    private void DetectObjectOnClick()
    {
        
        Ray ray = _mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, rayLength))
        {
            if (hit.collider != null)
            {
                IClick click = hit.collider.GetComponent<IClick>();
                if (click != null)
                {
                    click.OnClick();
                }
            } 
        }
        
        //Debug mode
        if (showRaysOnClick)
        {
            Debug.DrawRay(ray.origin, ray.direction * rayLength, Color.blue, 1f);
        }
    }
}