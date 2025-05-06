using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleController : MonoBehaviour
{
    public Camera mainCamera;

    private float yPosition;
    private float zPosition;

    private float minXPosition = -15.42f;
    private float maxXPosition = 19f;

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

        // Guardar la posición Y y Z del objeto actual
        yPosition = transform.position.y;
        zPosition = transform.position.z;
    }

    void Update()
    {
        float moveSpeed = 20f; // Puedes ajustar la velocidad según necesites
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
