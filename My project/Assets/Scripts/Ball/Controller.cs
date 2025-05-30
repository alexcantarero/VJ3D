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
    public bool stuck = false; // Indica si la concha est� pegada a la paleta
    public bool isSpedup = false;
    public bool isSlowDown = false;

    private float stuckOffsetX = 0f;
    private float stuckOffsetZ = 0f;
    private bool hasStuckOffset = false;

    private Rigidbody rb;

    public bool isFireMode = false; // Indica si la concha est� en modo "Fire"
    private Material originalMaterial; // Material original de la concha
    public Material fireMaterial; // Material rojo para el modo "Fire"

    private MeshRenderer shellBackRenderer; // MeshRenderer del hijo Shell_back
    public Texture fireTexture; // Textura para el modo "Fire"
    private Texture originalTexture; // Textura original del material en el �ndice 1

    private SphereCollider sC; // Collider de la esfera

    public Collider paddleCollider; // Colisionador de la paleta

    public ScoreDisplay scoreDisplay; // Asigna esto en el Inspector
    public int pointsPerBlock = 100;


    private List<Collider> ignoredBlockColliders = new List<Collider>();



    public AudioManager audioManager; 


    private void Awake()
    {
        paddleCollider = GameObject.FindGameObjectWithTag(paddleTag).GetComponent<Collider>();
        if(paddleCollider == null)
        {
            Debug.LogError("No se encontr� el objeto con la etiqueta " + paddleTag);
        }
        else Debug.Log(paddleCollider.gameObject.name);
        scoreDisplay = GameObject.Find("score").GetComponent<ScoreDisplay>();

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

    }
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        originalMaterial = GetComponent<Renderer>().material;
        sC = GetComponent<SphereCollider>();

        shellBackRenderer = gameObject.GetComponent<MeshRenderer>();

        if (shellBackRenderer != null && shellBackRenderer.materials.Length > 1)
        {
            originalTexture = shellBackRenderer.materials[1].mainTexture; // Guardar la textura original del material en el �ndice 1
        }

        float randomX;
        do
        {
            randomX = Random.Range(-1f, 1f);
        } while (Mathf.Abs(randomX) < 0.2f); // Evita que sea demasiado peque�o

        Vector3 direction = new Vector3(randomX, 0, 1).normalized;
        rb.velocity = direction * speed;
    }

    void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.CompareTag("Block")) {
            audioManager.PlaySFX(audioManager.shellBumpSFX);

            scoreDisplay.AddPoints(pointsPerBlock);
            //Debug.Log("puntos sumados!");

            if (!isFireMode)
            {
                // Destruir el bloque al atravesarlo
                //Debug.Log("Destruyendo bloque en modo Fire");
                Destroy(collision.gameObject);
            }
            else
            {
                return;
            }
        }
        else if (collision.gameObject.CompareTag(paddleTag))
        {
            PaddleController paddle = collision.gameObject.GetComponent<PaddleController>();
            if (paddle != null && paddle.sticky)
            {
                rb.velocity = Vector3.zero;
                stuck = true;

                // Usa el punto de contacto real
                Vector3 paddlePos = paddleCollider.transform.position;
                Vector3 contactPoint = collision.contacts[0].point;
                stuckOffsetX = contactPoint.x - paddlePos.x;
                stuckOffsetZ = contactPoint.z - paddlePos.z;
                hasStuckOffset = true;

                // Coloca la concha en el punto de contacto
                transform.position = new Vector3(contactPoint.x, transform.position.y, contactPoint.z);
            }
            else
            {
                CalculatePaddleBounce(collision);
            }
        }
        else
        {
            audioManager.PlaySFX(audioManager.shellBumpSFX);
            Vector3 incomingVelocity = rb.velocity;
            Vector3 normal = collision.contacts[0].normal;
            Vector3 reflectedVelocity = Vector3.Reflect(incomingVelocity, normal);

            rb.velocity = reflectedVelocity.normalized * speed;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (isFireMode && other.CompareTag("Block"))
        {
            scoreDisplay.AddPoints(pointsPerBlock);
            Destroy(other.gameObject);
        }
    }

    public void ActivateFireMode()
    {
        if (isFireMode) return;

        isFireMode = true;
        GetComponent<Renderer>().material = fireMaterial;
        gameObject.layer = LayerMask.NameToLayer("FireShell");

        // Ignorar colisiones f�sicas con los bloques
        SetFireModeCollisions(true);

        if (shellBackRenderer != null && fireTexture != null && shellBackRenderer.materials.Length > 1)
        {
            shellBackRenderer.materials[1].mainTexture = fireTexture;
        }

    }

    public void DeactivateFireMode()
    {
        isFireMode = false;
        GetComponent<Renderer>().material = originalMaterial;
        gameObject.layer = LayerMask.NameToLayer("Default");

        if (shellBackRenderer != null && originalTexture != null && shellBackRenderer.materials.Length > 1)
        {
            shellBackRenderer.materials[1].mainTexture = originalTexture;
        }

        // Restaurar colisiones f�sicas con los bloques
        Collider myCollider = GetComponent<Collider>();
        foreach (Collider blockCollider in ignoredBlockColliders)
        {
            if (blockCollider != null)
                Physics.IgnoreCollision(myCollider, blockCollider, false);
        }
        ignoredBlockColliders.Clear();
    }

    private void SetFireModeCollisions(bool ignore)
    {
        Collider myCollider = GetComponent<Collider>();
        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        foreach (GameObject block in blocks)
        {
            Collider blockCollider = block.GetComponent<Collider>();
            if (blockCollider != null)
            {
                Physics.IgnoreCollision(myCollider, blockCollider, ignore);
                if (ignore && !ignoredBlockColliders.Contains(blockCollider))
                    ignoredBlockColliders.Add(blockCollider);
            }
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

    void LaunchFromStuck()
    {

        if (paddleCollider == null) return;

        Vector3 paddleCenter = paddleCollider.transform.position;
        //Debug.Log(paddleCenter);
        float paddleWidth = paddleCollider.bounds.size.x;
        float offsetX = transform.position.x - paddleCenter.x;
        float normalizedOffset = offsetX / (paddleWidth * 0.5f);
        normalizedOffset = Mathf.Clamp(normalizedOffset, -1f, 1f);

        float bounceAngle = normalizedOffset * maxBounceAngle;
        Debug.Log("bounceAngle: " + bounceAngle);
        float angleRad = bounceAngle * Mathf.Deg2Rad;
        //Debug.Log($"offsetX: {offsetX}, normalizedOffset: {normalizedOffset}, bounceAngle: {bounceAngle}");
        float directionZ = 1f; // Siempre hacia arriba
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

        
        if(stuck)
        {
            if (paddleCollider != null && hasStuckOffset)
            {
                Vector3 paddlePos = paddleCollider.transform.position;
                transform.position = new Vector3(
                    paddlePos.x + stuckOffsetX,
                    transform.position.y,
                    paddlePos.z + stuckOffsetZ
                );
            }
            if (Input.GetKeyDown(KeyCode.Space))
            {
                stuck = false;
                hasStuckOffset = false;
                LaunchFromStuck();
            }

        }
    }



    void FixedUpdate()
    {
        if (!stuck) rb.velocity = rb.velocity.normalized * speed;
        if (transform.position.z < -18.0f)
        {
            FindObjectOfType<GameManager>().UnregisterBall();
            Destroy(gameObject);
        }
        if (isFireMode)
        {
            //Creo un radio de detecci�n
            float radioDeteccion = sC != null ? sC.radius * transform.localScale.x + 0.1f : 1.1f;
            Collider[] hits = Physics.OverlapSphere(transform.position, radioDeteccion);
            //Miro si colisiona con un bloque

            foreach (Collider hit in hits)
            {
                if (hit.CompareTag("Block"))
                {
                    BlockBehaviour blockBehaviour = hit.GetComponent<BlockBehaviour>();
                    if (blockBehaviour != null && !blockBehaviour.isBeingDestroyed)
                    {
                        scoreDisplay.AddPoints(pointsPerBlock);
                        Debug.Log("FixedUpdater");
                        blockBehaviour.DestroyByShell();
                    }
                    else if (blockBehaviour == null)
                    {
                        // Si no tiene BlockBehaviour, destr�yelo normalmente y suma puntos solo una vez
                        Destroy(hit.gameObject);
                        scoreDisplay.AddPoints(pointsPerBlock);
                    }
                }
            }
        }
    }
}