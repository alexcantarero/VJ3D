using UnityEngine;

public class MouseCameraOrbit : MonoBehaviour
{
    public Transform target;
    public float startDistance = 80f;
    public float endDistance = 20f;
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
    public float autoRotationSpeed = 20f;
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

        // Guardar la rotación objetivo desde el Inspector
        Vector3 originalEuler = transform.eulerAngles;
        targetX = originalEuler.y;
        targetY = originalEuler.x;

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;

        UpdateCameraPosition();

        Time.timeScale = 0f; // Escena empieza pausada
    }

    void Update()
    {
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

            x = Mathf.MoveTowards(x, targetX, currentRotationSpeed * Time.unscaledDeltaTime);
            y = Mathf.MoveTowards(y, targetY, currentRotationSpeed * Time.unscaledDeltaTime);

            float rotationDifference = Mathf.Abs(x - targetX) + Mathf.Abs(y - targetY);
            float zoomFactor = Mathf.InverseLerp(10f, 0f, rotationDifference);
            currentZoomSpeed *= Mathf.Lerp(1f, 10f, zoomFactor);

            currentDistance = Mathf.Lerp(currentDistance, endDistance, Time.unscaledDeltaTime * currentZoomSpeed);

            if (Mathf.Abs(currentDistance - endDistance) < 0.1f && Mathf.Abs(x - targetX) < 0.1f && Mathf.Abs(y - targetY) < 0.1f)
            {
                currentDistance = endDistance;
                isAutoRotating = false;
                Time.timeScale = 1f; // Reanuda el juego cuando la cámara termina de girar
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
        Vector3 negDistance = new Vector3(0.0f, 0.0f, -currentDistance);
        Vector3 position = rotation * negDistance + target.position;

        transform.rotation = rotation;
        transform.position = position;
    }
}
