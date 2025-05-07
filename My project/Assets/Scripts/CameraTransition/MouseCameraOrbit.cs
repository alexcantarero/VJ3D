using UnityEngine;

public class MouseCameraOrbit : MonoBehaviour
{
    public Transform target;
    public float startDistance = 80f;      // Distancia inicial
    public float endDistance = 60f;        // Distancia final
    public float xSpeed = 120.0f;
    public float ySpeed = 120.0f;
    public float yMinLimit = -20f;
    public float yMaxLimit = 80f;
    public float autoRotationSpeed = 20f;  // grados por segundo
    public float autoZoomSpeed = 10f;       // velocidad de acercamiento

    private float x = 0.0f;
    private float y = 0.0f;
    private float currentDistance;
    private float autoRotateTime = 0f;

    private bool isAutoRotating = false;

    private float initialX, initialY;
    private float targetX, targetY;
    private float speedMultiplier;


    void Start()
    {
        currentDistance = startDistance;

        // �ngulo inicial de c�mara: lateral
        x = 90f;
        y = -15f;

        // Guardar la rotaci�n original para volver a ella
        Vector3 originalEuler = transform.eulerAngles;
        targetX = originalEuler.y;
        targetY = originalEuler.x;

        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;

        UpdateCameraPosition();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1) && !isAutoRotating)
        {
            isAutoRotating = true;
            Time.timeScale = 0f; // Pausar juego
            autoRotateTime = 0f; // Reiniciar temporizador
        }

        if (isAutoRotating && target)
        {
            autoRotateTime += Time.unscaledDeltaTime / 2;

            // Aceleraci�n progresiva con el tiempo
            if (autoRotateTime < 1f)
            {
                speedMultiplier = 1.3f - autoRotateTime * 0.3f; // Aumenta 50% por segundo
            }
            float currentRotationSpeed = autoRotationSpeed * speedMultiplier;
            float currentZoomSpeed = autoZoomSpeed * speedMultiplier;

            // Rotaci�n progresiva
            x = Mathf.MoveTowards(x, targetX, currentRotationSpeed * Time.unscaledDeltaTime);
            y = Mathf.MoveTowards(y, targetY, currentRotationSpeed * Time.unscaledDeltaTime);

            // Si rotaci�n casi completada, aplicar boost al zoom
            float rotationDifference = Mathf.Abs(x - targetX) + Mathf.Abs(y - targetY);
            // Transici�n suave al zoom cuando rotaci�n est� casi completada
            float zoomFactor = Mathf.InverseLerp(10f, 0f, rotationDifference); // 1 cuando cerca, 0 cuando lejos
            currentZoomSpeed *= Mathf.Lerp(1f, 10f, zoomFactor);

            // Zoom progresivo (Interpolaci�n m�s suave de la distancia)
            currentDistance = Mathf.Lerp(currentDistance, endDistance, Time.unscaledDeltaTime * currentZoomSpeed);

            // Verifica si la c�mara ya lleg� a la posici�n final
            if (Mathf.Abs(currentDistance - endDistance) < 0.1f && Mathf.Abs(x - targetX) < 0.1f && Mathf.Abs(y - targetY) < 0.1f)
            {
                currentDistance = endDistance; // Ajustar para evitar un peque�o desfase
                isAutoRotating = false;
                Time.timeScale = 1f; // Reanudar juego
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
