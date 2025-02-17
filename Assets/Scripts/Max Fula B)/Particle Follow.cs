using System.Collections;
using System.Collections.Generic;
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
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = playerTransform.position + offset;
        if (player.GetComponent<BoatMovement>().moveSpeed > 0.1f)
        {
            //particleSystem.emission.SetBurst(0, )
            particleSystem.enableEmission = true;
//            Debug.Log("we movign blyat");
        }
        else
        {
            particleSystem.enableEmission = false;
        }
    }
}
