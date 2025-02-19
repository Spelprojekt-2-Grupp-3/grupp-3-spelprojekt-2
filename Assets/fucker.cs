using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fucker : MonoBehaviour
{
    [SerializeField]
    private ParticleSystem particles;

    public void whore()
    {
        particles.enableEmission = !particles.emission.enabled;
        //   particles.SetActive(!particles.active);
    }

    // Start is called before the first frame update
    void Start()
    {
        particles.enableEmission = false;
    }

    // Update is called once per frame
    void Update() { }
}
