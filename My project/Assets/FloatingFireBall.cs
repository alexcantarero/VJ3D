using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloatingFireBall : MonoBehaviour
{
    // Start is called before the first frame update
    public float amplitude = 0.5f;      // Qué tan alto se mueve
    public float speed = 2f;            // Qué tan rápido sube y baja
    private Vector3 startPos;

    void Start()
    {
        startPos = transform.position;
    }

    void Update()
    {
        float y = Mathf.Sin(Time.time * speed) * amplitude;
        transform.position = startPos + new Vector3(0, y, 0);
    }
}

