using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 25f;
    public float rotatingSpeed = 100f;
    public string paddleTag = "Paddle";
    public float maxBounceAngle = 50f; //En grados

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        float randomX;
        do
        {
            randomX = Random.Range(-1f, 1f);
        } while (Mathf.Abs(randomX) < 0.2f); // Evita que sea demasiado pequeño

        Vector3 direction = new Vector3(randomX, 0, 1).normalized;
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(paddleTag)) 
        {
            CalculatePaddleBounce(collision);
        }
        else 
        {
            
            Vector3 incomingVelocity = rb.velocity; 
            Vector3 normal = collision.contacts[0].normal;
            Vector3 reflectedVelocity = Vector3.Reflect(incomingVelocity, normal); 

            rb.velocity = reflectedVelocity.normalized * speed;
        }
    }
    void CalculatePaddleBounce(Collision collision)
    {
        Vector3 hitPoint = collision.contacts[0].point;
        Vector3 paddleCenter = collision.transform.position;

        float offsetX = hitPoint.x - paddleCenter.x;

        float paddleWidth = collision.collider.bounds.size.x;
        float normalizedOffset = offsetX / (paddleWidth * 0.5f);
        normalizedOffset = Mathf.Clamp(normalizedOffset, -1f, 1f);

        float bounceAngle = normalizedOffset * maxBounceAngle;

        float angleRad = bounceAngle * Mathf.Deg2Rad;

        float directionZ = Mathf.Sign(rb.velocity.z) * -1; 

        Vector3 direction = new Vector3(Mathf.Sin(angleRad), 0, Mathf.Cos(angleRad) * directionZ);

        rb.velocity = direction.normalized * speed;
    }

    void Update()
    {
        transform.Rotate(Vector3.up, rotatingSpeed * Time.deltaTime);

        if (Mathf.Abs(rb.velocity.magnitude - speed) > 0.01f) 
        {
            rb.velocity = rb.velocity.normalized * speed; 
        }
    }

    void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed;
    }
}
