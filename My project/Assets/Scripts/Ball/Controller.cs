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
        rb.useGravity = false;
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        Vector3 direction = new Vector3(0, 0, -1).normalized; // Mueve en diagonal XZ
        rb.velocity = direction * speed;

        Debug.Log("Velocidad inicial aplicada: " + rb.velocity);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisión con: " + collision.gameObject.name);


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

