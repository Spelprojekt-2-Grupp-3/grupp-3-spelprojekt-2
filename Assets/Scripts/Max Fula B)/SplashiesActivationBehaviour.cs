using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(ParticleSystem))]
public class SplashiesActivationBehaviour : MonoBehaviour
{
    [
        SerializeField,
        Tooltip(
            "Time in seconds between activation checks. Only runs while particleSystem is inactive. Default: 1"
        )
    ]
    private int waitTime = 1;

    [
        SerializeField,
        Tooltip(
            "Percentage chance of particle system reactivating on each activation check. Default: 1. With this setup an inactive system should take around 100 seconds to reactivate on average"
        )
    ]
    private int percentage = 1;
    private ParticleSystem pS;
    private Timer t;

    // Start is called before the first frame update
    void Start()
    {
        pS = GetComponent<ParticleSystem>();
        t = new Timer();
        activation = false;
        if (Random.Range(1, 11) >= 5)
        {
            pS.Stop();
        }
    }

    private bool activation = false;

    // Update is called once per frame
    void Update()
    {
        if (pS.isStopped && !activation)
        {
            activation = true;
            StartCoroutine(t.ExecuteAfterTime(waitTime, ActivationMayhaps));
        }
    }

    private void ActivationMayhaps()
    {
        if (Random.Range(1, 101) <= percentage)
        {
            pS.Play();
        }
      //  Debug.Log("Kalla mig laser mannen för mitt vapen har laser");

        activation = false;
    }
}
