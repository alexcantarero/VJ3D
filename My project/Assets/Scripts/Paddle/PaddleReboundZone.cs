using UnityEngine;

public class PaddleReboundZone : MonoBehaviour
{
    public float bounceForce = 10f;
    public float visualRecoilDistance = 0.2f;
    public float visualRecoilSpeed = 5f;

    private Vector3 originalLocalPosition;
    private bool isRecoiling = false;

    private Transform paddleVisual;

    void Start()
    {
        Transform found = transform.Find("Paddle");
        if (found != null)
        {
            paddleVisual = found;
            originalLocalPosition = paddleVisual.localPosition;
        }
        else
        {
            Debug.LogError("No se encontr� el objeto hijo llamado 'Paddle'. Aseg�rate de que exista y est� correctamente nombrado.");
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shell"))
        {
            Debug.Log("Colisi�n detectada con Shell en PaddleReboundZone");

            if (!isRecoiling && paddleVisual != null)
                StartCoroutine(RecoilAnimation());
        }
    }

    System.Collections.IEnumerator RecoilAnimation()
    {
        isRecoiling = true;
        float elapsed = 0f;
        Vector3 targetPosition = originalLocalPosition - transform.forward * visualRecoilDistance;

        // Movimiento hacia atr�s
        while (elapsed < 1f)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsed);
            paddleVisual.localPosition = Vector3.Lerp(originalLocalPosition, targetPosition, t);
            elapsed += Time.deltaTime * visualRecoilSpeed;
            yield return null;
        }

        elapsed = 0f;

        // Movimiento hacia adelante
        while (elapsed < 1f)
        {
            float t = Mathf.SmoothStep(0f, 1f, elapsed);
            paddleVisual.localPosition = Vector3.Lerp(targetPosition, originalLocalPosition, t);
            elapsed += Time.deltaTime * visualRecoilSpeed;
            yield return null;
        }

        paddleVisual.localPosition = originalLocalPosition;
        isRecoiling = false;
    }
}
