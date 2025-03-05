using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BuoyantObject : MonoBehaviour
{
    private readonly Color red = new(0.92f, 0.25f, 0.2f);
    private readonly Color green = new(0.2f, 0.92f, 0.51f);
    private readonly Color blue = new(0.2f, 0.67f, 0.92f);
    private readonly Color orange = new(0.97f, 0.79f, 0.26f);

    [Header("Water")]
    [SerializeField]
    private float waterHeight = 0.0f;

    public Material shaderMaterial;

    //Water Shader Settings
    [Header("Waves")]
    [SerializeField]
    float steepness;

    [SerializeField]
    float wavelength;

    [SerializeField]
    float speed;

    [SerializeField]
    float[] directions = new float[4];

    //End

    [Header("Buoyancy")]
    [Range(1, 8)]
    public float strength = 1f;

    private float defaultStrength;

    [Range(0.2f, 10)]
    public float objectDepth = 1f;

    public float velocityDrag = 0.99f;
    public float angularDrag = 0.5f;

    [Header("Effectors")]
    [Tooltip("Do not assign if not all effectors are children of this object")]
    public Transform effectorParent;
    public Transform[] effectors;

    private Rigidbody rb;
    private Vector3[] effectorProjections;

    private void Awake()
    {
        defaultStrength = strength;
        if (effectorParent != null)
        {
            List<Transform> stuff = new List<Transform>();
            foreach (Transform child in effectorParent)
            {
                stuff.Add(child);
            }
            effectors = new Transform[stuff.Count];
            for (int i = 0; i < effectors.Length; i++)
            {
                effectors[i] = stuff[i];
            }
        }

        // Get rigidbody
        rb = GetComponent<Rigidbody>();
        //rb.useGravity = false;

        steepness = shaderMaterial.GetFloat("_Wave_Steepness");
        wavelength = shaderMaterial.GetFloat("_Wave_Length");
        speed = shaderMaterial.GetFloat("_Wave_Speed");
        directions[0] = shaderMaterial.GetVector("_Wave_Directions").x;
        directions[1] = shaderMaterial.GetVector("_Wave_Directions").y;
        directions[2] = shaderMaterial.GetVector("_Wave_Directions").z;
        directions[3] = shaderMaterial.GetVector("_Wave_Directions").w;

        effectorProjections = new Vector3[effectors.Length];
        for (var i = 0; i < effectors.Length; i++)
            effectorProjections[i] = effectors[i].position;
    }

    private void OnDisable()
    {
        rb.useGravity = true;
    }

    [SerializeField]
    [Range(0.0f, 1f)]
    [Tooltip("Scalar value for how strongly the force increases")]
    private float scalarValue = 0.1f;

    [SerializeField]
    private float scalarThreshold = 2;

    public bool subm = false;

    private void FixedUpdate()
    {
        var effectorAmount = effectors.Length;

        for (var i = 0; i < effectorAmount; i++)
        {
            var effectorPosition = effectors[i].position;

            effectorProjections[i] = effectorPosition;
            effectorProjections[i].y =
                waterHeight
                + GerstnerWaveDisplacement
                    .GetWaveDisplacement(effectorPosition, steepness, wavelength, speed, directions)
                    .y;
            var waveHeight = effectorProjections[i].y;

            //height of each effector
            var effectorHeight = effectorPosition.y;

            float distanceY = Mathf.Abs(effectorHeight + 0.6f - waveHeight);
            float test = 1;
            if (distanceY >= scalarThreshold && transform.CompareTag("Player"))
            {
                test = 1 + scalarValue * distanceY;
                subm = false;
            }
            //  Debug.Log($"Scalar velocity {test}");
            // gravity
            rb.AddForceAtPosition(
                (Physics.gravity / effectorAmount) * test,
                effectorPosition,
                ForceMode.Acceleration
            );

            //Active wave height for each effector

            //  Debug.Log($"distance {i}: " + distanceY);
            //if the effector height is below the waves continue
            if (!(effectorHeight < waveHeight))
            {
                if (transform.CompareTag("Player"))
                {
                    strength = defaultStrength;
                }
                continue; // submerged
            }
            else
            {
//                Debug.Log("submerged");
                strength = 3;
                //strength = defaultStrength;
            }

            if (
                transform.position.y <= waterHeight + 0.4f
                && transform.CompareTag("Player")
                && !subm
            )
            {
                strength = 25;
                rb.velocity = rb.velocity / 1.1f;
                subm = true;
            }
            var submersion = Mathf.Clamp01(waveHeight - effectorHeight) / objectDepth;

            //  float BoatOffset = 2.8f;


            var buoyancy = Mathf.Abs(Physics.gravity.y) * submersion * strength;

            // buoyancy
            rb.AddForceAtPosition(
                Vector3.up * buoyancy * (test * 0.85f),
                effectorPosition,
                ForceMode.Acceleration
            );
            // drag
            rb.AddForce(
                -rb.velocity * (velocityDrag * Time.fixedDeltaTime),
                ForceMode.VelocityChange
            );

            // torque
            rb.AddTorque(
                -rb.angularVelocity * (angularDrag * Time.fixedDeltaTime),
                ForceMode.Impulse
            );
        }
    }

    private void OnDrawGizmos()
    {
        /*if (effectors == null)
            return;

        for (var i = 0; i < effectors.Length; i++)
        {
            if (!Application.isPlaying && effectors[i] != null)
            {
                Gizmos.color = green;
                Gizmos.DrawSphere(effectors[i].position, 0.06f);
            }
            else
            {
                if (effectors[i] == null)
                    return;

                Gizmos.color = effectors[i].position.y < effectorProjections[i].y ? red : green; // submerged

                Gizmos.DrawSphere(effectors[i].position, 0.06f);

                Gizmos.color = orange;
                Gizmos.DrawSphere(effectorProjections[i], 0.06f);

                Gizmos.color = blue;
                Gizmos.DrawLine(effectors[i].position, effectorProjections[i]);
            }
        }*/
    }
}
