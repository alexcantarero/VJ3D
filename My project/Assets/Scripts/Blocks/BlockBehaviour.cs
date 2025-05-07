using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{

    public GameObject powerUpPrefab;

    void Start()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shell"))
        {
            //Animaci�n de destrucci�n
            int valor = Random.Range(0,9);
            Debug.Log( "Valor: " + valor);
            if (valor == 8)  spawnPowerup();//Spawnear powerup (1/3 de chance)
            Destroy(gameObject, 0.1f); //Destrucci�n con delay

        }
    }

    void spawnPowerup()
    {
        if (powerUpPrefab != null)
        {
            Vector3 spawnPosition = transform.position; //Posici�n del bloque
            Instantiate(powerUpPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("PowerUp prefab no se ha asignado.");
        }
    }

    void Update()
    {

    }


}
