using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RampBehaviour : MonoBehaviour
{
    [SerializeField]
    private int targetAngle = 28;

    [SerializeField]
    private float floatingScalarValue = 0.005f;

    [SerializeField]
    private float waitTime = 2;
    private int defaultTiltAngle;
    private float defaultScalarValue;

    private GameObject g;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boat"))
        {
            g = collision.gameObject;
            BuoyantObject boatBuoyancy = g.GetComponent<BuoyantObject>();
            BoatMovement bM = g.GetComponent<BoatMovement>();
            defaultScalarValue = boatBuoyancy.scalarValue;
            boatBuoyancy.scalarValue = floatingScalarValue;
            defaultTiltAngle = bM.frontTiltAngle;
            bM.frontTiltAngle = targetAngle;
            Timer t = new Timer();
            foreach (var v in g.GetComponentsInChildren<ParticleFollow>())
            {
                v.ShouldEmit = false;
            }

            StartCoroutine(t.ExecuteAfterTime(waitTime, Reactivate));
        }
    }

    void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Boat"))
        {
            BoatMovement bM = collision.gameObject.GetComponent<BoatMovement>();
            bM.frontTiltAngle = defaultTiltAngle;
        }
    }

    private void Reactivate()
    {
        g.GetComponent<BuoyantObject>().scalarValue = defaultScalarValue;
    }
}
