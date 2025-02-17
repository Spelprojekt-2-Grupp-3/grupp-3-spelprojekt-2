using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleActivationBehaviour : MonoBehaviour
{
    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter(Collider collider)
    {
        GetComponent<SphereCollider>().enabled = false;
        gameObject.GetComponentInChildren<KrakenLocation>().OnActivation();
    }
}
