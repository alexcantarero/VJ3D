using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController : MonoBehaviour
{
    public float speed = 10f;  // Velocidad inicial
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        Vector3 direction = new Vector3(1, 0, 1).normalized; // Mueve en diagonal XZ
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("holaquetal");
        
        Vector3 incomingVelocity = rb.velocity;
        Vector3 normal = collision.contacts[0].normal;
        Vector3 reflectedVelocity = Vector3.Reflect(incomingVelocity, normal);

        rb.velocity = reflectedVelocity.normalized * speed;
    }
    

    // Update is called once per frame
    void Update()
    {
        
        
    }


}

