using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicGroundCheck : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    public LayerMask groundLayer;

    private Animator animator;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        bool grounded = Physics.Raycast(
            transform.position + new Vector3(0, 1, 0),
            Vector3.down,
            5f,
            groundLayer
        );
        animator.SetBool("Grounded", grounded);
        Debug.Log(grounded);
    }
}
