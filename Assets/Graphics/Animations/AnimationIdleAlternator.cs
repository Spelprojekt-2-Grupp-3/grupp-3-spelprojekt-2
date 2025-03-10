using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationIdleAlternator : MonoBehaviour
{
    private Animator _ani;

    // Start is called before the first frame update
    void Start()
    {
        _ani = GetComponent<Animator>();
    }

    public void Check()
    {
        float i = Mathf.RoundToInt(Random.Range(0, 6));
        if (i == 5)
        {
            _ani.SetTrigger("Fidget");
           // Debug.Log("Fidget");
        }
        else
        {
            float f = Random.Range(8, 12) / 10f;
            _ani.SetFloat("IdleSpeed", f);
          //  Debug.Log(f);
           // Debug.Log("Repeat");
        }
    }
}
