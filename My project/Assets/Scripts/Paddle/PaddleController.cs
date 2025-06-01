using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject shellPrefab; // Prefab de la concha
    public GameObject billPrefab;
    public GameObject magnet;

    public ParticleSystem leftExpl;
    public ParticleSystem rightExpl;

    public GameObject Lcannon;
    public GameObject Rcannon;

    public GameObject LBillPoint;
    public GameObject RBillPoint;

    private BoxCollider playerCollider;

    public bool tripled = false;
    public bool sticky = false;
    public bool isBig = false;

    private float yPosition;
    private float zPosition;

    private float minXPositionSmall = -18f;
    private float maxXPositionSmall = 18f;

    private float minXPositionBig = -15f;
    private float maxXPositionBig = 15f;

    public int initialBlockCount = 0;
    public float percentageBlocksDestroyed = 0;
    public ScoreDisplay scoreDisplay; 

    public AudioManager audioManager;


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

        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        initialBlockCount = blocks.Length;

        playerCollider = GetComponent<BoxCollider>();
        if(playerCollider == null)
        {
            Debug.LogError("No BoxCollider found on the object. Please ensure the paddle has a BoxCollider component.");
        }

        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();

    }

    void OnTriggerEnter(Collider other)
    {
        audioManager.PlaySFX(audioManager.powerupSFX);
        if (other.gameObject.tag == "TripleShroom")
        {
            Debug.Log("TripleShroom");
            scoreDisplay.AddPoints(300);
            SpawnTwoShells();
        }
        else if (other.gameObject.tag == "FireFlower")
        {
            Debug.Log("FireFlower");
            scoreDisplay.AddPoints(900);
            TurnShellRed();
        }
        else if (other.gameObject.tag == "IceFlower")
        { 
            Debug.Log("IceFlower");
            scoreDisplay.AddPoints(200);
            TurnShellGreen();
        }
        else if (other.gameObject.tag == "MegaMushroom")
        {
            Debug.Log("MegaMushroom");
            scoreDisplay.AddPoints(500);
            AugmentPaddleX();

        }
        else if (other.gameObject.tag == "MiniMushroom")
        {
            Debug.Log("MiniMushroom");
            scoreDisplay.AddPoints(700);
            if (isBig) ShrinkPaddleX();
        }
        else if (other.gameObject.tag == "BulletBill")
        {
            Debug.Log("BulletBill");
            scoreDisplay.AddPoints(300);
            StartCoroutine(ShootBulletBill());

        }
        else if (other.gameObject.tag == "MagnetShroom")
        {
            Debug.Log("MagnetShroom");
            scoreDisplay.AddPoints(1200);
            StickyPaddle();

        }
        else if (other.gameObject.tag == "+Clock")
        {
            Debug.Log("+Clock");
            scoreDisplay.AddPoints(500);
            SpeedUpShells();
        }
        else if (other.gameObject.tag == "-Clock")
        {
            Debug.Log("-Clock");
            scoreDisplay.AddPoints(500);
            SlowShells();
        }
        else if (other.gameObject.tag == "Star")
        {
            Debug.Log("Star");
            scoreDisplay.AddPoints(1000);
            FinishGame();
        }

        Destroy(other.gameObject); // Destruir el powerup
    }

    public void SpawnTwoShells()
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
        FindObjectOfType<GameManager>().RegisterBall();

        GameObject newShell2 = Instantiate(shellPrefab, shellPositionInPlay, Quaternion.identity);
        FindObjectOfType<GameManager>().RegisterBall();

    }

    public void TurnShellRed()
    {
        Controller[] shells = FindObjectsOfType<Controller>(); // Encontrar todas las conchas activas
        foreach (Controller shell in shells)
        {
            shell.ActivateFireMode();
        }
    }

    public void TurnShellGreen()
    {
        Controller[] shells = FindObjectsOfType<Controller>(); // Encontrar todas las conchas activasç
        foreach (Controller shell in shells)
        {
            shell.DeactivateFireMode();
        }

    }

    public void AugmentPaddleX()
    {
        if (!isBig)
        {
            isBig = true;
            Transform paddle = transform.Find("Paddle");
            paddle.transform.localScale = new Vector3(paddle.transform.localScale.x, paddle.transform.localScale.y * 2.2f, paddle.transform.localScale.z);
            Lcannon.transform.position = new Vector3(Lcannon.transform.position.x + 2.81f, Lcannon.transform.position.y, Lcannon.transform.position.z);
            Rcannon.transform.position = new Vector3(Rcannon.transform.position.x - 3.8f, Rcannon.transform.position.y, Rcannon.transform.position.z);
            playerCollider.size = new Vector3(playerCollider.size.x * 2, playerCollider.size.y, playerCollider.size.z);
            playerCollider.center = new Vector3(playerCollider.center.x * 2 / 2, playerCollider.center.y, playerCollider.center.z);

        }

    }

    public void ShrinkPaddleX()
    {
        if (isBig)
        {

            isBig = false;
            Transform paddle = transform.Find("Paddle");
            paddle.transform.localScale = new Vector3(paddle.transform.localScale.x, paddle.transform.localScale.y / 2.2f, paddle.transform.localScale.z);
            Lcannon.transform.position = new Vector3(Lcannon.transform.position.x - 2.81f, Lcannon.transform.position.y, Lcannon.transform.position.z);
            Rcannon.transform.position = new Vector3(Rcannon.transform.position.x + 3.8f, Rcannon.transform.position.y, Rcannon.transform.position.z);
            playerCollider.size = new Vector3(playerCollider.size.x / 2, playerCollider.size.y, playerCollider.size.z);
            playerCollider.center = new Vector3(playerCollider.center.x / 2, playerCollider.center.y, playerCollider.center.z);

        }

    }

    public IEnumerator ShootBulletBill()
    {
        for (int i = 0; i < 3; i++)
        {
            // Dispara dos balas
            audioManager.PlaySFX(audioManager.bulletBillSFX);
            Instantiate(billPrefab, LBillPoint.transform.position, Quaternion.identity);
            Instantiate(billPrefab, RBillPoint.transform.position, Quaternion.identity);

            leftExpl.Play();
            rightExpl.Play();

            if (i < 2) // Espera solo después de las dos primeras tandas
                yield return new WaitForSeconds(2f);
        }

    }

    public void StickyPaddle()
    {

        foreach (Controller shell in FindObjectsOfType<Controller>())
        {
            shell.stuck = true; // Cambia la variable stuck a true
        }
        sticky = true;
        magnet.SetActive(true);

    }

    public void SpeedUpShells()
    {
        Controller[] shells = FindObjectsOfType<Controller>();
        foreach (Controller shell in shells)
        {
            if (!shell.isSpedup)
            {
                shell.speed *= 1.3f; // Aumenta la velocidad de las conchas un 30%
                shell.isSpedup = true;
            }
        }
    }

    public void SlowShells()
    {
        Controller[] shells = FindObjectsOfType<Controller>();
        foreach (Controller shell in shells)
        {
            if (!shell.isSlowDown)
            {
                shell.speed *= 0.7f; // Disminuye la velocidad de las conchas un 30%
                if(!shell.isSpedup) shell.isSlowDown = true; //Si iba a velocidad normal, decelera
                else shell.isSpedup = false; // Si estaba acelerada, se desactiva la aceleración
            }
        }
    }

    void FinishGame()
    {
        Debug.Log("¡Juego terminado!");
        FindObjectOfType<GameManager>().WinGame();
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
        float clampedX;
        if(!isBig) clampedX = Mathf.Clamp(newX, minXPositionSmall, maxXPositionSmall);
        else clampedX = Mathf.Clamp(newX, minXPositionBig, maxXPositionBig);

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

        GameObject[] blocks = GameObject.FindGameObjectsWithTag("Block");
        percentageBlocksDestroyed = (initialBlockCount - blocks.Length) / (float)initialBlockCount * 100f;

    }
}

