using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject shellPrefab; // Prefab de la concha
    public GameObject billPrefab;

    public GameObject Lcannon;
    public GameObject Rcannon;

    public GameObject LBillPoint;
    public GameObject RBillPoint;

    public bool tripled = false;
    public bool sticky = false;
    public bool isBig = false;

    private float yPosition;
    private float zPosition;

    private float minXPosition = -16.42f;
    private float maxXPosition = 20f;


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
        else if (other.gameObject.tag == "MiniMushroom")
        {
            Debug.Log("MiniMushroom");
            if (isBig) ShrinkPaddleX();
        }
        else if (other.gameObject.tag == "BulletBill")
        {
            Debug.Log("BulletBill");
            StartCoroutine(ShootBulletBill());

        }
        else if (other.gameObject.tag == "MagnetShroom")
        {
            Debug.Log("MagnetShroom");
            StickyPaddle();
        
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

        tripled = true;

        Controller[] shells = FindObjectsOfType<Controller>();
        if (shells.Length == 0)
        {
            Debug.LogWarning("No hay conchas activas para clonar.");
            return;
        }

        GameObject shellInPlay = shells[0].gameObject;
        Vector3 shellPositionInPlay = shellInPlay.transform.position;

        GameObject newShell1 = Instantiate(shellPrefab, shellPositionInPlay, Quaternion.identity);
        GameObject newShell2 = Instantiate(shellPrefab, shellPositionInPlay, Quaternion.identity);

        Controller originalController = shellInPlay.GetComponent<Controller>();
        bool godModeActive = originalController != null && originalController.IsGodModeEnabled();

        Collider sharedPaddle = originalController.paddleCollider;
        GameObject sharedWall = originalController.invisibleWall;

        Controller controller1 = newShell1.GetComponent<Controller>();
        Controller controller2 = newShell2.GetComponent<Controller>();

        if (controller1 != null)
        {
            controller1.paddleCollider = sharedPaddle;
            controller1.invisibleWall = sharedWall;

            if (godModeActive) controller1.ToggleGodMode(true);
        }

        if (controller2 != null)
        {
            controller2.paddleCollider = sharedPaddle;
            controller2.invisibleWall = sharedWall;

            if (godModeActive) controller2.ToggleGodMode(true);
        }
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
        if (!isBig)
        {
            isBig = true;
            Transform paddle = transform.Find("Paddle");
            paddle.transform.localScale = new Vector3(paddle.transform.localScale.x, paddle.transform.localScale.y * 2.2f, paddle.transform.localScale.z);
            Lcannon.transform.position = new Vector3(Lcannon.transform.position.x + 2.81f, Lcannon.transform.position.y, Lcannon.transform.position.z);
            Rcannon.transform.position = new Vector3(Rcannon.transform.position.x - 3.8f, Rcannon.transform.position.y, Rcannon.transform.position.z);

        }

    }

    void ShrinkPaddleX()
    {
        if (isBig)
        {

            isBig = false;
            Transform paddle = transform.Find("Paddle");
            paddle.transform.localScale = new Vector3(paddle.transform.localScale.x, paddle.transform.localScale.y / 2.2f, paddle.transform.localScale.z);
            Lcannon.transform.position = new Vector3(Lcannon.transform.position.x - 2.81f, Lcannon.transform.position.y, Lcannon.transform.position.z);
            Rcannon.transform.position = new Vector3(Rcannon.transform.position.x + 3.8f, Rcannon.transform.position.y, Rcannon.transform.position.z);

        }

    }

    IEnumerator ShootBulletBill()
    {
        for (int i = 0; i < 3; i++)
        {
            // Dispara dos balas
            Instantiate(billPrefab, LBillPoint.transform.position, Quaternion.identity);
            Instantiate(billPrefab, RBillPoint.transform.position, Quaternion.identity);

            if (i < 2) // Espera solo después de las dos primeras tandas
                yield return new WaitForSeconds(2f);
        }

    }

    void StickyPaddle()
    {
        sticky = true;
    }


    void MovePaddle()
    {
        float moveSpeed = 40f;
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
        MovePaddle();
    }


    private void FixedUpdate()
    {

        //Comprobaciones de conchas
        Controller[] shells = FindObjectsOfType<Controller>();
        //Debug.Log("Hay " + shells.Length);

        if (shells.Length == 1)
        {
            tripled = false; //Si hay una sola concha, significa que puede salir powerup triple
            //Debug.Log("Concha única");
        }
        if (shells.Length == 0) Time.timeScale = 0f; //Pausar juego si no hay conchas





    }
}

