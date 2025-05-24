using UnityEngine;
using System.Collections;

public class CameraShake : MonoBehaviour
{
    public static CameraShake Instance;
    public float duration = 0.1f; // Duration of the shake
    public float magnitude = 0.05f; // Magnitude of the shake

    private Vector3 initialPosition;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void Shake()
    {
        StartCoroutine(ShakeCoroutine());
    }

    private IEnumerator ShakeCoroutine()
    {
        initialPosition = transform.localPosition;

        float elapsed = 0f;
        float freq = 0.05f;

        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float y = Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = initialPosition + new Vector3(x, y, 0);

            elapsed += freq;
            yield return new WaitForSeconds(freq);
        }

        transform.localPosition = initialPosition;
    }
}
