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

        // Establece una direcci�n inicial, por ejemplo, diagonal hacia adelante
        Vector3 direction = new Vector3(-1, 0, 1).normalized;
        rb.velocity = direction * speed;

        Debug.Log("Velocidad inicial aplicada: " + rb.velocity);
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log("Colisi�n con: " + collision.gameObject.name);

        // Direcci�n de la velocidad entrante
        Vector3 incomingVelocity = rb.velocity;

        // Normal de la superficie con la que la bola colision�
        Vector3 normal = collision.contacts[0].normal;

        // Calcular la velocidad reflejada
        Vector3 reflectedVelocity = Vector3.Reflect(incomingVelocity, normal);

        // Ajusta la velocidad de rebote para que no pierda demasiada velocidad
        rb.velocity = reflectedVelocity.normalized * speed;

        // Tambi�n puedes a�adir algo de fricci�n o ajuste de rebote si lo deseas
        // Ejemplo: se podr�a aumentar la velocidad si se encuentra un "material especial"
        // Si la bola colisiona con objetos especiales (como rampas), podr�as aplicar efectos extra.
    }

    void Update()
    {
        // Aqu� podr�as agregar m�s l�gica para la bola si es necesario
    }
}
