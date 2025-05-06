using UnityEngine;

public class MouseCameraOrbit : MonoBehaviour
{
    public Transform target;          // Objeto a orbitar (puede ser la bola o una escena vac�a)
    public float distance = 60;    // Distancia desde la c�mara al target
    public float xSpeed = 120.0f;     // Sensibilidad horizontal
    public float ySpeed = 120.0f;     // Sensibilidad vertical

    public float yMinLimit = -20f;    // L�mite inferior de rotaci�n vertical
    public float yMaxLimit = 80f;     // L�mite superior de rotaci�n vertical

    private float x = 0.0f;
    private float y = 0.0f;

    void Start()
    {
        Vector3 angles = transform.eulerAngles;
        x = angles.y;
        y = angles.x;

        // Evita que la f�sica interfiera con la c�mara
        if (GetComponent<Rigidbody>())
            GetComponent<Rigidbody>().freezeRotation = true;
    }

    void LateUpdate()
    {
        if (target && (Input.GetMouseButton(1)))
        {
            x += Input.GetAxis("Mouse X") * xSpeed * 0.02f;
            y -= Input.GetAxis("Mouse Y") * ySpeed * 0.02f;

            y = Mathf.Clamp(y, yMinLimit, yMaxLimit);

            Quaternion rotation = Quaternion.Euler(y, x, 0);
            Vector3 negDistance = new Vector3(0.0f, 0.0f, -distance);
            Vector3 position = rotation * negDistance + target.position;

            transform.rotation = rotation;
            transform.position = position;
        }
    }
}
