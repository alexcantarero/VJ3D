using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallController2 : MonoBehaviour
{
    public float speed = 10f;  // Velocidad inicial
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        rb.useGravity = false; // Desactivar gravedad si no la deseas
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;

        // Establece una dirección inicial, por ejemplo, diagonal hacia adelante
        Vector3 direction = new Vector3(-1, 0, 1).normalized;
        rb.velocity = direction * speed;

        Debug.Log("Velocidad inicial aplicada: " + rb.velocity);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisión con: " + collision.gameObject.name);

        // Dirección de la velocidad entrante
        Vector3 incomingVelocity = rb.velocity;

        // Normal de la superficie con la que la bola colisionó
        Vector3 normal = collision.contacts[0].normal;

        // Calcular la velocidad reflejada
        Vector3 reflectedVelocity = Vector3.Reflect(incomingVelocity, normal);

        // Ajusta la velocidad de rebote para que no pierda demasiada velocidad
        rb.velocity = reflectedVelocity.normalized * speed;

        // También puedes añadir algo de fricción o ajuste de rebote si lo deseas
        // Ejemplo: se podría aumentar la velocidad si se encuentra un "material especial"
        // Si la bola colisiona con objetos especiales (como rampas), podrías aplicar efectos extra.
    }

    void Update()
    {
        // Aquí podrías agregar más lógica para la bola si es necesario
    }
}
