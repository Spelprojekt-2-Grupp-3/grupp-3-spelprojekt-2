using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public class AnimationRigTransform : MonoBehaviour
{
    // Start is called before the first frame update
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
    private AnimationRigTransform sibling;

    [HideInInspector]
    public bool grounded;

    [SerializeField]
    private Transform IdleTransformPos;

    [SerializeField]
    private Transform runningTransformRef;
    private Transform transformToMatch;

    private string ActiveClipName;

    [SerializeField]
    private TwoBoneIKConstraint AniRigConstraint;

    // Update is called once per frame
    void Update()
    {
        ActiveClipName = cleo.GetComponent<Animator>().GetCurrentAnimatorClipInfo(0)[0].clip.name;
        //Debug.Log(ActiveClipName);
        if (ActiveClipName == "Run750")
        {
            transformToMatch = runningTransformRef;
            //Running pos
        }
        else if (ActiveClipName == "Idle01")
        {
            transformToMatch = IdleTransformPos;
            //IdlePos
        }
        Debug.Log(ActiveClipName);
        transform.localPosition = transformToMatch.localPosition;
        transform.localEulerAngles = transformToMatch.localEulerAngles;

        if (cleo != null && ActiveClipName == "Idle01")
        {
            //            Debug.Log("Running Cleo Idle Functionality");
            RaycastHit hit;
            //WE ARE PLAY PLAY CLIPPO
            //YAHOOWAHOO I MISS MY WIFE, TAILS
            float hipY = hipTransform.position.y;
            Vector3 startPos = new Vector3(transform.position.x, hipY, transform.position.z);
            if (Physics.Raycast(startPos, Vector3.down, out hit, maxDistance, layerMask))
            {
                AniRigConstraint.weight = 1;
                Debug.DrawRay(startPos, Vector3.down * hit.distance, Color.magenta);
                transform.position = hit.point + FootOffsetValue;
                //Debug.Log(hit.collider.gameObject.name);
                flipped = false;
                grounded = true;
            }
            else
            {
                AniRigConstraint.weight = 0;
                if (sibling.grounded)
                {
                    sibling.fuuuck.enabled = false;
                    fuuuck.enabled = true;
                }

                grounded = false;
                Debug.DrawRay(startPos, Vector3.down * maxDistance, Color.cyan);
                /*  if (!flipped)
                  {
                      fuuuck.enabled = true;
                      //cleo.GetComponent<CollissionHeightComparison>().FlipActation();
                      flipped = true;
                      Debug.Log("In air, flipping ");
                  }*/
            }

            //need to make sure te correct one is active if subtle change is made
            // cleo.GetComponent<CollissionHeightComparison>().CheckPos();
        }
        else if (cleo != null && ActiveClipName == "Run750")
        {
            AniRigConstraint.weight = 0;
        }
        else
        {
            AniRigConstraint.weight = 0;
        }
    }
}
