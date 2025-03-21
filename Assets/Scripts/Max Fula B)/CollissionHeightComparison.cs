using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.Dependencies.Sqlite;
using UnityEngine;

public class CollissionHeightComparison : MonoBehaviour
{
    [SerializeField]
    private GameObject R_Col;

    [SerializeField]
    private GameObject L_Col;

    [SerializeField]
    private Animator animator;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update()
    {
        if (R_Col.GetComponent<BoxCollider>().enabled == L_Col.GetComponent<BoxCollider>().enabled)
        {
            CheckPos();
        }
        //Debug.Log(animator.GetCurrentAnimatorClipInfo(0)[0].clip.name);
    }

    public void CheckPos()
    {
        if (R_Col.transform.position.y > L_Col.transform.position.y)
        {
            R_Col.GetComponent<BoxCollider>().enabled = false;
            L_Col.GetComponent<BoxCollider>().enabled = true;
        }
        else if (R_Col.transform.position.y < L_Col.transform.position.y)
        {
            R_Col.GetComponent<BoxCollider>().enabled = true;
            L_Col.GetComponent<BoxCollider>().enabled = false;
        }
    }

    public void FlipActation()
    {
        //This runs regardless, I am restarted
        if (R_Col.GetComponent<BoxCollider>().enabled == L_Col.GetComponent<BoxCollider>().enabled)
            return;
        bool r = R_Col.GetComponent<BoxCollider>().enabled;
        bool l = L_Col.GetComponent<BoxCollider>().enabled;

        R_Col.GetComponent<BoxCollider>().enabled = l;
        L_Col.GetComponent<BoxCollider>().enabled = r;
    }
}
