using UnityEngine;

public class PaddleReboundZone : MonoBehaviour
{
    public float bounceForce = 10f;
    public float visualRecoilDistance = 0.2f;
    public float visualRecoilSpeed = 5f;

    private Vector3 originalLocalPosition;
    private bool isRecoiling = false;

    void Start()
    {
        originalLocalPosition = transform.localPosition;
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shell"))
        {
            Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
            if (rb != null)
            {
                // Rebote físico
                Vector3 reboundDirection = Vector3.up + Vector3.forward;
                rb.velocity = reboundDirection.normalized * bounceForce;

                // Inicia animación de retroceso visual
                if (!isRecoiling)
                    StartCoroutine(RecoilAnimation());

                Debug.Log("Rebote físico + visual desde la pala.");
            }
        }
    }

    System.Collections.IEnumerator RecoilAnimation()
    {
        isRecoiling = true;
        float elapsed = 0f;
        Vector3 targetPosition = originalLocalPosition - transform.forward * visualRecoilDistance;

        // Ir hacia atrás
        while (elapsed < 1f)
        {
            transform.localPosition = Vector3.Lerp(originalLocalPosition, targetPosition, elapsed);
            elapsed += Time.deltaTime * visualRecoilSpeed;
            yield return null;
        }

        elapsed = 0f;
        // Volver a posición original
        while (elapsed < 1f)
        {
            transform.localPosition = Vector3.Lerp(targetPosition, originalLocalPosition, elapsed);
            elapsed += Time.deltaTime * visualRecoilSpeed;
            yield return null;
        }

        transform.localPosition = originalLocalPosition;
        isRecoiling = false;
    }
}
