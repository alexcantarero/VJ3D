using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 50f;
    public float rotatingSpeed = 100f;
    public string paddleTag = "Paddle";
    public float maxBounceAngle = 75f; //En grados

    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        float randomX = Random.Range(-1f, 1f); //Posición de movimiento inicial

        Vector3 direction = new Vector3(randomX, 0, 1).normalized;
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag(paddleTag)) //Si choca contra la paleta
        {
            CalculatePaddleBounce(collision);
        }
        else //Contra cualquier otra cosa
        {
            
            Vector3 incomingVelocity = rb.velocity; //Velocidad con la que colisiona
            Vector3 normal = collision.contacts[0].normal;
            Vector3 reflectedVelocity = Vector3.Reflect(incomingVelocity, normal); //Reflejo de la velocidad

            // Mantener la velocidad constante
            rb.velocity = reflectedVelocity.normalized * speed;
        }
    }
    //REVISAR------------------------------------------------------
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
        //Rotación de la concha sobre sí misma
        transform.Rotate(Vector3.up, rotatingSpeed * Time.deltaTime);

        // Velocidad constante
        if (Mathf.Abs(rb.velocity.magnitude - speed) > 0.01f) // Si la velocidad es mayor que speed
        {
            rb.velocity = rb.velocity.normalized * speed; //Recalculamos la velocidad
        }
    }

    void FixedUpdate()
    {
        // Asegurar que la velocidad es exactamente la que queremos
        rb.velocity = rb.velocity.normalized * speed;
    }
}
