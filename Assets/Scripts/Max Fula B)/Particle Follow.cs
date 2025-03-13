using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ParticleFollow : MonoBehaviour
{
    public Vector3 offset;
    public GameObject player;
    public Transform playerTransform;

    private ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        defaultLifetime = particleSystem.startLifetime;
        defaultEmissionRate = particleSystem.emissionRate;
        defaultRotation = particleSystem.transform.rotation;
    }

    float defaultLifetime;
    float playerMovementSpeed;
    float defaultEmissionRate;
    float scalarValue;
    private Quaternion defaultRotation;

    [SerializeField]
    private float lowerLimit;

    // Update is called once per frame
    void Update()
    {
        
        playerMovementSpeed = player.GetComponent<BoatMovement>().moveSpeed;
        // transform.position = playerTransform.position + offset;
        if (playerMovementSpeed > 0.1f)
        {
            scalarValue = playerMovementSpeed / 700;
            //particleSystem.emission.SetBurst(0, )
            particleSystem.enableEmission = true;
            //            Debug.Log("we movign blyat");
            //   Debug.Log(playerMovementSpeed);
            var emi = particleSystem.emission;
            emi.rateOverTime = defaultEmissionRate * scalarValue;

            Debug.Log(emi.rateOverTime.constant);
            particleSystem.startLifetime = defaultLifetime * (1.25f - scalarValue);
            if (transform.position.y < lowerLimit)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    lowerLimit,
                    transform.position.z
                );
            }
        }
        else
        {
            particleSystem.enableEmission = false;
        }
    }
}
