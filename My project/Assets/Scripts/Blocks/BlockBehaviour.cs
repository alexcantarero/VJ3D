using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{

    public GameObject TripleShroomPrefab;
    public GameObject FireFlowerPrefab;
    public GameObject MegaMushroomPrefab;
    public GameObject MiniSetaPrefab;

    private PaddleController pC;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        pC = player.GetComponent<PaddleController>();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shell") || collision.gameObject.CompareTag("BulletBill"))
        {
            //Animación de destrucción
            int valor = Random.Range(0,9); //1/8 de chance
            if (valor == 8)
            {
                Debug.Log("Valor: " + valor);
                int powerup = Random.Range(0, 3);

                switch (powerup) {
                    case 0: //Caso tripled
                        if (!pC.tripled) spawnPowerupSeta();
                        break;

                    case 1: //Caso fireFlower
                        spawnPowerupFireFlower();
                        break;

                    case 2: //Caso isBig
                        if (!pC.isBig) spawnPowerupMegaMushroom();
                        else {
                            spawnPowerupMiniSeta();
                        }
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
    void spawnPowerupMegaMushroom()
    {

        if (MegaMushroomPrefab != null)
        {
            Vector3 spawnPosition = transform.position;
            Instantiate(MegaMushroomPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("MegaMushroom no se ha asignado.");
        }
    }
    void spawnPowerupMiniSeta()
    {

        if (MiniSetaPrefab != null)
        {
            Vector3 spawnPosition = transform.position;
            Instantiate(MiniSetaPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("MiniSeta no se ha asignado.");
        }
    }

    void Update()
    {

    }


}
