using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Controller : MonoBehaviour
{
    public float speed = 25f;
    public float rotatingSpeed = 100f;
    public string paddleTag = "Paddle";
    public float maxBounceAngle = 50f; // En grados
    private Rigidbody rb;

    public bool isFireMode = false; // Indica si la concha está en modo "Fire"
    private Material originalMaterial; // Material original de la concha
    public Material fireMaterial; // Material rojo para el modo "Fire"

    private MeshRenderer shellBackRenderer; // MeshRenderer del hijo Shell_back
    public Texture fireTexture; // Textura para el modo "Fire"
    private Texture originalTexture; // Textura original del material en el índice 1

    private SphereCollider sC; // Collider de la esfera

    private bool isGodModeActive = false; // Variable que controla el God Mode
    public Collider paddleCollider; // Colisionador de la paleta
    public GameObject invisibleWall; // Pared invisible

    public ScoreDisplay scoreDisplay; // Asigna esto en el Inspector
    public int pointsPerBlock = 100;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalMaterial = GetComponent<Renderer>().material;
        sC = GetComponent<SphereCollider>();

        // Desactivar la pared invisible al principio
        if (invisibleWall != null)
        {
            invisibleWall.SetActive(false);
        }

        shellBackRenderer = gameObject.GetComponent<MeshRenderer>();

        if (shellBackRenderer != null && shellBackRenderer.materials.Length > 1)
        {
            originalTexture = shellBackRenderer.materials[1].mainTexture; // Guardar la textura original del material en el índice 1
        }

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
        if (isGodModeActive)
        {
            return;
        }
        if (collision.gameObject.CompareTag("Block")) {
            scoreDisplay.AddPoints(pointsPerBlock);
            Debug.Log("puntos sumados!");
            
            if (!isFireMode)
            {
                // Destruir el bloque al atravesarlo
                Debug.Log("Destruyendo bloque en modo Fire");
                Destroy(collision.gameObject);
            }
            else 
            {
                return;
            }
        }
        else if (collision.gameObject.CompareTag(paddleTag))
        {
            // Rebote en la paleta
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

    public void ToggleGodMode(bool isActive)
    {

        Collider[] ballColliders = GetComponentsInChildren<Collider>();
        Collider[] paddleColliders = paddleCollider.GetComponentsInChildren<Collider>();

        

        if (isActive)
        {
            // Desactivar la colisión de la paleta
            if (paddleCollider != null)
            {
                foreach (var ballCol in ballColliders)
                {
                    foreach (var padCol in paddleColliders)
                    {
                        Physics.IgnoreCollision(ballCol, padCol, true);
                    }
                }
            }

            // Activar la pared invisible
            if (invisibleWall != null)
            {
                invisibleWall.SetActive(true); // Activar la pared invisible para que las pelotas colisionen con ella
            }
        }
        else
        {
            // Restaurar las colisiones
            if (paddleCollider != null)
            {
                foreach (var ballCol in ballColliders)
                {
                    foreach (var padCol in paddleColliders)
                    {
                        Physics.IgnoreCollision(ballCol, padCol, false);
                    }
                }
            }

            // Desactivar la pared invisible
            if (invisibleWall != null)
            {
                invisibleWall.SetActive(false); // Desactivar la pared invisible
            }
        }
    }

    public void EnableGodMode()
    {
        isGodModeActive = true;
        ToggleGodMode(true);
        Debug.Log("God Mode enabled on: " + gameObject.name);

    }

    public bool IsGodModeEnabled()
    {
        return isGodModeActive;
    }


    public void ActivateFireMode(float duration)
    {
        if (isFireMode) return; // Evitar activar el modo "Fire" si ya está activo

        isFireMode = true;
        GetComponent<Renderer>().material = fireMaterial; // Cambiar a material rojo

        // Cambiar la textura del material en el índice 1 de Shell_back
        if (shellBackRenderer != null && fireTexture != null && shellBackRenderer.materials.Length > 1)
        {
            shellBackRenderer.materials[1].mainTexture = fireTexture;
        }

        // Desactivar el modo "Fire" después de la duración
        StartCoroutine(DeactivateFireMode(duration));
    }

    private IEnumerator DeactivateFireMode(float duration)
    {
        yield return new WaitForSeconds(duration);
        isFireMode = false;
        GetComponent<Renderer>().material = originalMaterial; // Restaurar el material original

        // Restaurar la textura original del material en el índice 1 de Shell_back
        if (shellBackRenderer != null && originalTexture != null && shellBackRenderer.materials.Length > 1)
        {
            shellBackRenderer.materials[1].mainTexture = originalTexture;
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
  
        // Toggle God Mode cuando se presiona la tecla 'G'
        if (Input.GetKeyDown(KeyCode.G))
        {
            isGodModeActive = !isGodModeActive; // Alternar el estado del God Mode
            ToggleGodMode(isGodModeActive);
        }

        /*if (isGodModeActive) 
            Debug.Log("God Mode activado");
        else
            Debug.Log("God Mode incativo"); */
    }

    void FixedUpdate()
    {
        rb.velocity = rb.velocity.normalized * speed;
        if (transform.position.z < -18.0f)
        {

            Destroy(gameObject);
        }
    }
}