using UnityEngine;

public class MouseCameraOrbit : MonoBehaviour
{
    public Transform target;
    public float startDistance = 50f;
    public float endDistance = 45f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
    public float autoRotationSpeed = 10f;
    public float autoZoomSpeed = 10f;

    private float x = 0.0f;
    private float y = 0.0f;
    private float currentDistance;
    private float autoRotateTime = 0f;

    private bool isAutoRotating = false;
    private bool hasStarted = false; // Nuevo: controla si se presionó '1'

    private float targetX, targetY;
    private float speedMultiplier;

    void Start()
    {
        currentDistance = startDistance;

        // Ángulo inicial de cámara: lateral
        x = 90f;
        y = -15f;

        targetX = 0f;
        targetY = 50f;

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;

        UpdateCameraPosition();

        Time.timeScale = 0f; // Escena empieza pausada
    }

    void Update()
    {
        Debug.Log($"Distance to target: {Vector3.Distance(transform.position, target.position)}");

        // Espera a que el jugador presione '1' para empezar la animación y reanudar el juego
        if (!hasStarted && Input.GetKeyDown(KeyCode.Alpha1))
        {
            hasStarted = true;
            isAutoRotating = true;
            autoRotateTime = 0f;
            Time.timeScale = 0f; // Sigue pausado, animación corre con unscaledDeltaTime
        }

        if (isAutoRotating && target)
        {
            autoRotateTime += Time.unscaledDeltaTime / 2;

            if (autoRotateTime < 1f)
            {
                speedMultiplier = 1.3f - autoRotateTime * 0.3f;
            }

            float currentRotationSpeed = autoRotationSpeed * speedMultiplier;
            float currentZoomSpeed = autoZoomSpeed * speedMultiplier;

            // Move rotation towards target values
            x = Mathf.MoveTowards(x, targetX, currentRotationSpeed * Time.unscaledDeltaTime);
            y = Mathf.MoveTowards(y, targetY, currentRotationSpeed * Time.unscaledDeltaTime);

            float rotationDifference = Mathf.Abs(x - targetX) + Mathf.Abs(y - targetY);
            float zoomFactor = Mathf.InverseLerp(10f, 0f, rotationDifference);
            currentZoomSpeed *= Mathf.Lerp(1f, 10f, zoomFactor);

            // Move distance towards endDistance smoothly (towards target zoom value)
            currentDistance = Mathf.MoveTowards(currentDistance, endDistance, Time.unscaledDeltaTime * currentZoomSpeed);

            Debug.Log($"Current Distance: {currentDistance}, Target Distance: {endDistance}"); // Debugging line

            // Check if rotation is complete
            bool rotationDone = Mathf.Abs(x - targetX) < 0.1f && Mathf.Abs(y - targetY) < 0.1f;

            // Zoom is done when currentDistance reaches endDistance
            bool zoomDone = Mathf.Abs(currentDistance - endDistance) < 0.1f;

            // If rotation is done, snap to the target
            if (rotationDone)
            {
                x = targetX;
                y = targetY;
            }

            // If zoom is done, snap to the target distance
            if (zoomDone)
            {
                currentDistance = endDistance;
            }

            // Once both rotation and zoom are done, stop auto-rotation and resume the game
            if (rotationDone && zoomDone)
            {
                isAutoRotating = false;
                Time.timeScale = 1f; // Reanuda el juego
            }

            UpdateCameraPosition();
        }
    }

    void LateUpdate()
    {
        if (target && !isAutoRotating && Input.GetMouseButton(1))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;
            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            UpdateCameraPosition();
        }
    }

    void UpdateCameraPosition()
    {
        Quaternion rotation = Quaternion.Euler(y, x, 0);
        Vector3 direction = rotation * Vector3.back;
        transform.position = target.position + direction * currentDistance;
        transform.rotation = rotation;
    }

}
