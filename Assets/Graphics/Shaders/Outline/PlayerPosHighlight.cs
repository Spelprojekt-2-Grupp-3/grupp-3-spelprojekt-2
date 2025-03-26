using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerPosHighlight : MonoBehaviour
{
    [SerializeField]
    private Material mat;

    // Start is called before the first frame update
    void Awake() { 
        mat.SetFloat("_CutoffRange", 0);
    }

    // Update is called once per frame
    void Update()
    {
        mat.SetVector("_PlayerPos", transform.position);
    }
}
