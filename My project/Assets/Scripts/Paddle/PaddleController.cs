using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject shellPrefab; // Prefab de la concha
    public GameObject invisibleWall; // Pared invisible

    public GameObject Lcannon;
    public GameObject Rcannon;

    public bool tripled = false;
    public bool isBig = false;

    private float yPosition;
    private float zPosition;

    private float minXPosition = -16.42f;
    private float maxXPosition = 20f;

    private bool isGodModeActive = false; // Variable que controla el God Mode
    private Collider paddleCollider; // Colisionador de la paleta

    void Start()
    {
        if (mainCamera == null)
        {
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("No main camera found. Please assign a camera to the mainCamera field.");
            }
        }

        yPosition = transform.position.y;
        zPosition = transform.position.z;

        paddleCollider = GetComponent<BoxCollider>(); // Obtener el colisionador de la paleta

        // Desactivar la pared invisible al principio
        if (invisibleWall != null)
        {
            invisibleWall.SetActive(false);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "TripleShroom")
        {
            Debug.Log("TripleShroom");
            SpawnTwoShells(other.gameObject);
        }
        else if (other.gameObject.tag == "FireFlower")
        {
            Debug.Log("FireFlower");
            TurnShellRed();
        }
        else if (other.gameObject.tag == "MegaMushroom")
        { 
            Debug.Log("MegaMushroom");
            AugmentPaddleX();

        }
            Destroy(other.gameObject); // Destruir el powerup
    }

    void SpawnTwoShells(GameObject shell)
    {
        if (shellPrefab == null)
        {
            Debug.LogError("Shell prefab no se ha asignado.");
            return;
        }
        tripled = true; // Activar el modo triple
        Vector3 shellPosition = shellPrefab.transform.position; //Posición de la concha
        Instantiate(shellPrefab, shellPosition, Quaternion.identity);
        Instantiate(shellPrefab, shellPosition, Quaternion.identity);
    }
    void TurnShellRed()
    {
        Controller[] shells = FindObjectsOfType<Controller>(); // Encontrar todas las conchas activas
        foreach (Controller shell in shells)
        {
            shell.ActivateFireMode(5f); // Activar el modo "Fire" durante 3 segundos
        }
    }

    void AugmentPaddleX()
    {
        isBig = true;
        Transform paddle = transform.Find("Paddle");
        paddle.transform.localScale = new Vector3(paddle.transform.localScale.x, paddle.transform.localScale.y*2.2f,paddle.transform.localScale.z);
        Lcannon.transform.position = new Vector3(Lcannon.transform.position.x + 2.81f, Lcannon.transform.position.y, Lcannon.transform.position.z);
        Rcannon.transform.position = new Vector3(Rcannon.transform.position.x - 3.8f, Rcannon.transform.position.y, Rcannon.transform.position.z);

    }

    void ShrinkPaddleX()
    {
        Transform paddle = transform.Find("Paddle");
        paddle.transform.localScale = new Vector3(paddle.transform.localScale.x, paddle.transform.localScale.y / 2.2f, paddle.transform.localScale.z);
        Lcannon.transform.position = new Vector3(Lcannon.transform.position.x - 2.81f, Lcannon.transform.position.y, Lcannon.transform.position.z);
        Rcannon.transform.position = new Vector3(Rcannon.transform.position.x + 3.8f, Rcannon.transform.position.y, Rcannon.transform.position.z);

    }



    void ToggleGodMode(bool isActive)
    {
        if (isActive)
        {
            // Desactivar la colisión de la paleta
            if (paddleCollider != null)
            {
                paddleCollider.enabled = false; // Desactivar la colisión de la paleta
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
                paddleCollider.enabled = true; // Activar la colisión
            }

            // Desactivar la pared invisible
            if (invisibleWall != null)
            {
                invisibleWall.SetActive(false); // Desactivar la pared invisible
            }
        }
    }

    void MovePaddle()
    {
        float moveSpeed = 30f;
        float moveInput = 0f;

        if (Input.GetKey(KeyCode.LeftArrow))
        {
            moveInput = -1f;
        }
        else if (Input.GetKey(KeyCode.RightArrow))
        {
            moveInput = 1f;
        }

        float newX = transform.position.x + moveInput * moveSpeed * Time.deltaTime;
        float clampedX = Mathf.Clamp(newX, minXPosition, maxXPosition);

        transform.position = new Vector3(clampedX, yPosition, zPosition);
    }

    void Update()
    {
        // Toggle God Mode cuando se presiona la tecla 'G'
        if (Input.GetKeyDown(KeyCode.G))
        {
            isGodModeActive = !isGodModeActive; // Alternar el estado del God Mode
            ToggleGodMode(isGodModeActive);
        }

        if (isGodModeActive);
        //Debug.Log("God Mode activado");
        else; //Debug.Log("God Mode incativo");


        MovePaddle();
    }


    private void FixedUpdate()
    {

        //Comprobaciones de conchas
        Controller[] shells = FindObjectsOfType<Controller>();
        Debug.Log("Hay " + shells.Length);

        if (shells.Length == 1)
        {
            tripled = false; //Si hay una sola concha, significa que puede salir powerup triple
            Debug.Log("Concha única");
        }
        foreach (Controller shell in shells)
        {
            if (shell.gameObject.transform.position.z < -18.0f) //Si se sale de la escena
            {
                Destroy(shell); //Destruir la concha
                Debug.Log("Concha destruida por salir de la escena");
                if (shells.Length == 0) Time.timeScale = 0f; //Pausar juego si no hay conchas
                //return;
            }
        }
    }
}

