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
            // NO modificar velocidad, solo animar
            if (!isRecoiling)
                StartCoroutine(RecoilAnimation());

            //Debug.Log("Solo animaci�n de retroceso, sin alterar f�sica.");
        }
    }

    System.Collections.IEnumerator RecoilAnimation()
    {
        isRecoiling = true;
        float elapsed = 0f;
        Vector3 targetPosition = originalLocalPosition - transform.forward * visualRecoilDistance;

        // Ir hacia atr�s
        while (elapsed < 1f)
        {
            transform.localPosition = Vector3.Lerp(originalLocalPosition, targetPosition, elapsed);
            elapsed += Time.deltaTime * visualRecoilSpeed;
            yield return null;
        }

        elapsed = 0f;
        // Volver a posici�n original
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
