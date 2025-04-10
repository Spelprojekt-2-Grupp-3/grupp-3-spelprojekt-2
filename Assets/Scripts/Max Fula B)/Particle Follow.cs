using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class ParticleFollow : MonoBehaviour
{
    public GameObject player;
    Vector3 defaultLocalPos;

    public enum type
    {
        back,
        front,
        side
    }

    public type particleType;

    public float UpperLimit = 0;
    private ParticleSystem particleSystem;

    // Start is called before the first frame update
    void Start()
    {
        particleSystem = GetComponent<ParticleSystem>();
        defaultLifetime = particleSystem.startLifetime;
        defaultEmissionRate = particleSystem.emissionRate;
        defaultRotation = particleSystem.transform.rotation;
        //defaultX = transform.position.x;
        defaultLocalPos = transform.localPosition;
    }

    float defaultLifetime;
    float playerMovementSpeed;
    float defaultEmissionRate;
    float scalarValue;
    private Quaternion defaultRotation;

    [SerializeField]
    [Tooltip("Clamps particle motion to never go below this world Y value")]
    private float lowerLimit;

    public bool ShouldEmit = true;

    // Update is called once per frame
    void Update()
    {
        if (!ShouldEmit)
        {
            particleSystem.enableEmission = false;
            return;
        }
        //We get the active movement speed of the player
        playerMovementSpeed = player.GetComponent<BoatMovement>().moveSpeed;
        if (playerMovementSpeed > 0.1f)
        {
            //We create a scalar value from the maximum possible boat speed (boosting)
            scalarValue = playerMovementSpeed / 700;

            switch (particleType)
            {
                case type.back:
                {
                    particleSystem.enableEmission = true; //legacy particle code because it fucking works
                    break;
                }
                case type.front:
                {
                    if (playerMovementSpeed > 345)
                    {
                        particleSystem.enableEmission = true;
                    }
                    else
                    {
                        particleSystem.enableEmission = false;
                    }
                    break;
                }
                case type.side:
                {
                    if (playerMovementSpeed > 250)
                    {
                        particleSystem.enableEmission = true;
                    }
                    else
                        particleSystem.enableEmission = false;
                    break;
                }
                default:
                {
                    Debug.LogWarning("ParticleType not defined");
                    break;
                }
            }

            var emi = particleSystem.emission; //we reference the emission
            emi.rateOverTime = defaultEmissionRate * scalarValue; //we update the emmission based on movement speed, faster speed means faster emmission

            particleSystem.startLifetime = defaultLifetime * (1.25f - scalarValue); //We do basically the same with lifetime but reversed. The faster you move the faster your particles die

            //Clamp particle movement
            if (transform.position.y < lowerLimit || transform.position.y > UpperLimit)
            {
                transform.position = new Vector3(
                    transform.position.x,
                    lowerLimit,
                    transform.position.z
                );
                transform.localPosition = defaultLocalPos;
            }
        }
        else
        {
            //if we're not moving, don't emit
            particleSystem.enableEmission = false;
        }
    }
}
