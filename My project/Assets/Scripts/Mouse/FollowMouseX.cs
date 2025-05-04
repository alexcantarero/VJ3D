using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowMouseX : MonoBehaviour
{
    public Camera mainCamera;

    private float yPosition;
    private float zPosition;
    private float minXPosition = -11.13f;
    private float maxXPosition = 10f;

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
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        Plane plane = new Plane(Vector3.up, new Vector3(0, yPosition, 0));

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 hitPoint = ray.GetPoint(distance);
            float clampedX = Mathf.Clamp(hitPoint.x, minXPosition, maxXPosition);

            transform.position = new Vector3(clampedX, yPosition, zPosition);
        }
    }
}
