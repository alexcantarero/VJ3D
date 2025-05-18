using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{

    public GameObject TripleShroomPrefab;
    public GameObject FireFlowerPrefab;
    public GameObject MegaMushroomPrefab;
    public GameObject MiniSetaPrefab;
    public GameObject BulletBillPWPrefab;
    public GameObject MagnetSetaPrefab;

    private PaddleController pC;
    private Controller c;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        pC = player.GetComponent<PaddleController>();
        c = player.GetComponent<Controller>();
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shell") || collision.gameObject.CompareTag("BulletBill"))
        {
            //Animación de destrucción
            if(collision.gameObject.CompareTag("BulletBill"))
            {
                Destroy(collision.gameObject);
            }
  
            int valor = Random.Range(0,9); //1/8 de chance
            if (valor == 8)
            {
                Debug.Log("Valor: " + valor);
                int powerup = Random.Range(0, 5);

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
                    case 3:
                        spawnBulletBillPowerup();
                        break;
                    case 4:
                        if (!pC.sticky) spawnPowerupMagnetSeta();
                        else spawnBulletBillPowerup();
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

    void spawnBulletBillPowerup()
    {
        if (BulletBillPWPrefab != null)
        {
            Vector3 spawnPosition = transform.position;
            Instantiate(BulletBillPWPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("BulletBill no se ha asignado.");
        }
    }

    void spawnPowerupMagnetSeta()
    {
        if (MagnetSetaPrefab != null)
        {
            Vector3 spawnPosition = transform.position;
            Instantiate(MagnetSetaPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("BulletBill no se ha asignado.");
        }
    }

    void Update()
    {

    }


}
