using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 50f;  // Velocidad inicial
    public float rotatingSpeed = 100f; // Velocidad de rotación
    public string paddleTag = "Paddle"; // Tag para identificar la paleta
    public float maxBounceAngle = 75f; // Ángulo máximo de rebote en grados

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        float randomX = Random.Range(-1f, 1f);

        Vector3 direction = new Vector3(randomX, 0, 1).normalized; // Mueve en diagonal XZ
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(paddleTag))
        {
            // Colisión con la paleta - ángulo variable
            CalculatePaddleBounce(collision);
        }
        else
        {
            // Colisión normal con otras superficies
            Vector3 incomingVelocity = rb.velocity;
            Vector3 normal = collision.contacts[0].normal;
            Vector3 reflectedVelocity = Vector3.Reflect(incomingVelocity, normal);

            // Mantener la velocidad constante
            rb.velocity = reflectedVelocity.normalized * speed;
        }
    }

    void CalculatePaddleBounce(Collision collision)
    {
        // Obtiene el punto de contacto y el centro de la paleta
        Vector3 hitPoint = collision.contacts[0].point;
        Vector3 paddleCenter = collision.transform.position;

        // Calcular la posición relativa del punto de impacto en la paleta
        // Solo consideramos el eje X, ya que la paleta se mueve horizontalmente
        float offsetX = hitPoint.x - paddleCenter.x;

        // Normalizar el offset para que esté entre -1 y 1
        // Asumimos que paddleWidth es el ancho de la paleta
        float paddleWidth = collision.collider.bounds.size.x;
        float normalizedOffset = offsetX / (paddleWidth * 0.5f);
        normalizedOffset = Mathf.Clamp(normalizedOffset, -1f, 1f);

        // Calcular el ángulo de rebote basado en la posición de impacto
        float bounceAngle = normalizedOffset * maxBounceAngle;

        // Convertir el ángulo a radianes y crear un vector de dirección
        float angleRad = bounceAngle * Mathf.Deg2Rad;

        // Determinar la dirección Z (hacia adelante o hacia atrás)
        // Si la bola viene de abajo, rebota hacia arriba y viceversa
        float directionZ = Mathf.Sign(rb.velocity.z) * -1; // Invertir la dirección Z

        // Crear el vector de dirección con el nuevo ángulo
        Vector3 direction = new Vector3(Mathf.Sin(angleRad), 0, Mathf.Cos(angleRad) * directionZ);

        // Aplicar la nueva velocidad manteniendo la misma magnitud
        rb.velocity = direction.normalized * speed;
    }

    // Update is called once per frame
    void Update()
    {
        // Rotación visual (si quieres reactivarla)
        transform.Rotate(Vector3.up, rotatingSpeed * Time.deltaTime);

        // Forzar que la velocidad sea siempre exactamente "speed"
        if (Mathf.Abs(rb.velocity.magnitude - speed) > 0.01f)
        {
            rb.velocity = rb.velocity.normalized * speed;
        }
    }

    // FixedUpdate es mejor para físicas
    void FixedUpdate()
    {
        // Asegurar que la velocidad es exactamente la que queremos
        rb.velocity = rb.velocity.normalized * speed;
    }
}