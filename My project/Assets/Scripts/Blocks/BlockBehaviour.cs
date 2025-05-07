using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{

    public GameObject TripleShroomPrefab;
    public GameObject FireFlowerPrefab;

    void Start()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shell"))
        {
            //Animación de destrucción
            int valor = Random.Range(0,9);
            
            if (valor == 8)
            {
                Debug.Log("Valor: " + valor);
                int powerup = Random.Range(0, 3);
                switch (powerup) {
                    case 0:
                        spawnPowerupSeta();
                        break;

                    case 1:
                        spawnPowerupFireFlower();
                        break;

                    case 2:
                        spawnPowerupSeta();
                        break;

                }
            }
            Destroy(gameObject, 0.1f);
        }
    }

    void spawnPowerupSeta()
    {
        if (TripleShroomPrefab != null)
        {
            Vector3 spawnPosition = transform.position;
            Instantiate(TripleShroomPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Seta no se ha asignado.");
        }
    }

    void spawnPowerupFireFlower()
    {

        if (FireFlowerPrefab != null)
        {
            Vector3 spawnPosition = transform.position;
            Instantiate(FireFlowerPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Fireflower no se ha asignado.");
        }
    }

    void Update()
    {

    }


}
