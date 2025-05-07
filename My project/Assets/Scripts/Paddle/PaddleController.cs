using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject shellPrefab; // Prefab de la concha
    //public GameObject movingShell; // Prefab de la concha

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
        if (other.gameObject.name == "TripleShroomPowerup")
        {
            // Triplicar las conchas
            SpawnTwoShells(other.gameObject);
            Destroy(other.gameObject); // Destruir el powerup
        }
    }

    void SpawnTwoShells(GameObject shell)
    { 

        if(shellPrefab == null)
        {
            Debug.LogError("Shell prefab no se ha asignado.");
            return;
        }

        Vector3 shellPosition = shellPrefab.transform.position; //Posición de la concha
        Instantiate(shellPrefab, shellPosition, Quaternion.identity);
        Instantiate(shellPrefab, shellPosition, Quaternion.identity);

    }

    void Update()
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

}
