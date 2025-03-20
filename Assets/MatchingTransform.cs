using System.Collections;
using System.Collections.Generic;
using Ink.Parsed;
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

    // Start is called before the first frame update
    void Start() { }

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
}
