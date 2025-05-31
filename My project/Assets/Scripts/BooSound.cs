using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BooSound : MonoBehaviour
{

    public AudioManager audioManager;
    private float booTimer = 0f;
    private float booInterval = 5f; // Intervalo entre sonidos de Boo
    private void Awake()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        if (audioManager == null)
        {
            Debug.LogError("AudioManager not found in the scene. Please ensure it is present.");
        }
    }
    void Start()
    {
        
    }
    void Update()
    {
        booTimer += Time.deltaTime;
        if (booTimer >= booInterval)
        {
            audioManager.PlaySFX(audioManager.booSFX);
            booTimer = 0f;
            booInterval = Random.Range(5f, 8f); // Nuevo intervalo aleatorio

        }
    }
}
