using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BasicGroundCheck : MonoBehaviour
{
    public float force;
    public LayerMask groundLayer;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }
    [SerializeField, Range(0, 5)]
    private float rnge;
    // Update is called once per frame
    void FixedUpdate()
    {
        RaycastHit hit;
        bool grounded = Physics.Raycast(
            transform.position + new Vector3(0, 1, 0),
            Vector3.down,
            out hit,
            rnge,
            groundLayer
        );
        Debug.DrawRay(transform.position + new Vector3(0, 1, 0), Vector3.down * rnge, Color.white);
        animator.SetBool("Grounded", grounded);
        if (!grounded)
        {
            GetComponent<Rigidbody>().AddForce(Vector3.down * force, ForceMode.Force);
        }
    }
}
