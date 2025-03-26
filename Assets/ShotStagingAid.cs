using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class ShotStagingAid : MonoBehaviour
{
    [SerializeField]
    private bool ActivateStaging;

    [SerializeField]
    private GameObject UI;

    // Start is called before the first frame update
    void Start()
    {
        if (ActivateStaging)
        {
            UI.SetActive(false);
            GetComponent<CinemachineFreeLook>().Priority = 11;
        }
    }

    // Update is called once per frame
    void Update() { }
}
