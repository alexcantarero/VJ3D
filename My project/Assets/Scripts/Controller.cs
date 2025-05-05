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

    // Update is called once per frame
    void Update()
    {
        
    }


}

