using System.Collections;
using System.Collections.Generic;
using System.Net.Cache;
using Ink.Parsed;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;

public class MatchingTransform : MonoBehaviour
{
    [SerializeField]
    private Transform transformToMatch;

    [SerializeField]
    private Space space;

    [Header("just... just press the ones, I couldn't get a MaskField to work properly")]
    [SerializeField]
    private bool position;

    [SerializeField]
    private bool rotation;

    [SerializeField]
    private bool scale;

    private enum Space
    {
        Local,
        World
    };

    [SerializeField, Header("Assign only if you need to reference the player, used for ANIRIG")]
    private GameObject cleo;

    [SerializeField]
    private Transform hipTransform;

    // Start is called before the first frame update
    void Start() { }

    [SerializeField]
    float maxDistance = 100;

    [SerializeField]
    private LayerMask layerMask;

    private Vector3 FootOffsetValue = new Vector3(0, 1.07f, 0);

    bool flipped;

    [SerializeField]
    public BoxCollider fuuuck;

    [SerializeField]
    private MatchingTransform sibling;

    [HideInInspector]
    public bool grounded;

    // Update is called once per frame
    void Update()
    {
        if (space == Space.Local)
        {
            if (position)
                transform.localPosition = transformToMatch.localPosition;
            if (rotation)
                transform.localEulerAngles = transformToMatch.localEulerAngles;
            if (scale)
                transform.localScale = transformToMatch.localScale;
        }
        else if (space == Space.World)
        {
            if (position)
                transform.position = transformToMatch.position;
            if (rotation)
                transform.rotation = transformToMatch.rotation;
            if (scale)
                transform.localScale = transformToMatch.localScale;
        }
        

            //need to make sure te correct one is active if subtle change is made
            // cleo.GetComponent<CollissionHeightComparison>().CheckPos();
        }
    }

 

