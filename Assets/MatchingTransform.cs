using System.Collections;
using System.Collections.Generic;
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
    }

    void FixedUpdate()
    {
        if (cleo != null)
        {
            RaycastHit hit;
            //WE ARE PLAY PLAY CLIPPO
            //YAHOOWAHOO I MISS MY WIFE, TAILS
            float hipY = hipTransform.position.y;
            Vector3 startPos = new Vector3(transform.position.x, hipY, transform.position.z);
            if (Physics.Raycast(startPos, Vector3.down, out hit, maxDistance, layerMask))
            {
                Debug.DrawRay(startPos, Vector3.down * hit.distance, Color.magenta);
                Debug.Log("Hit");
            }
            else
            {
                Debug.DrawRay(startPos, Vector3.down * maxDistance, Color.cyan);

                Debug.Log("no hit");
            }

            /*
                OK so I find hip bone
                I find hip local Y pos,
                send ray from foot position, offset with hip y. range to slightly below feet level
                Find contact world position
                move target to contact position

                Separate manager script
                Compare collision object world space y
                Deactivate higher collider
        
            */
        }
    }
}
