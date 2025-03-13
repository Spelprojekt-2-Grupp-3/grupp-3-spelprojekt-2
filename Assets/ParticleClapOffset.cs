using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleClapOffset : MonoBehaviour
{
    private ParticleSystem partSys = null;

    // Start is called before the first frame update
    void Start()
    {
        partSys = GetComponent<ParticleSystem>();
    }

    private ParticleSystem.Particle[] particles;

    // Update is called once per frame
    void Update()
    {
        particles = new ParticleSystem.Particle[partSys.particleCount];
        partSys.GetParticles(particles);
        for (int i = 0; i < partSys.particleCount; i++)
        {
            Debug.Log(particles[i].position.y);
        }
    }
}
