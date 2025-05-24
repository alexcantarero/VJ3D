using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{

    private GameManager gm;

    public GameObject TripleShroomPrefab;
    public GameObject FireFlowerPrefab;
    public GameObject IceFlowerPrefab;
    public GameObject MegaMushroomPrefab;
    public GameObject MiniSetaPrefab;
    public GameObject BulletBillPWPrefab;
    public GameObject MagnetSetaPrefab;
    public GameObject PlusClockPrefab;
    public GameObject MinusClockPrefab;
    public GameObject StarPrefab;

    private PaddleController pC;
    private Controller c;

    public bool isBeingDestroyed = false;

    void Start()
    {
        GameObject player = GameObject.Find("Player");
        pC = player.GetComponent<PaddleController>();

        GameObject gameManager = GameObject.Find("GameManager");
        gm = gameManager.GetComponent<GameManager>();
        gm?.RegisterBlock();


        GameObject shell = GameObject.FindGameObjectWithTag("Shell"); //Coge cualquier shell

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
            DestroyByShell();
        }
        
    }

    public void DestroyByShell()
    {
        if (isBeingDestroyed) return; // Evita múltiples ejecuciones
        isBeingDestroyed = true;
        CameraShake.Instance.Shake(); // ← Aquí

        // Lógica de powerups y estrella para modo fuego
        if (pC.percentageBlocksDestroyed >= 95 && !gm.starSpawned)
        {
            spawnStar();
            gm.starSpawned = true;
        }
        else
        {
            int valor = Random.Range(0, 5);
            if (valor == 3)
            {
                int powerup = Random.Range(0, 6);
                switch (powerup)
                {
                    case 0:
                        if (!pC.tripled) spawnPowerupSeta();
                        break;
                    case 1:
                        Controller shell1 = FindObjectOfType<Controller>();
                        if (!shell1.isFireMode) spawnPowerupFireFlower();
                        else spawnPowerupIceFlower();
                        break;
                    case 2:
                        if (!pC.isBig) spawnPowerupMegaMushroom();
                        else spawnPowerupMiniSeta();
                        break;
                    case 3:
                        spawnBulletBillPowerup();
                        break;
                    case 4:
                        if (!pC.sticky) spawnPowerupMagnetSeta();
                        else spawnBulletBillPowerup();
                        break;
                    case 5:
                        Controller shell = FindObjectOfType<Controller>();
                        if (!shell.isSpedup && !shell.isSlowDown)
                            spawnPlusClock();
                        else if (shell.isSpedup) spawnMinusClock();
                        else if (shell.isSlowDown) spawnPlusClock();
                        else spawnMinusClock();
                        break;
                }
            }
        }

        gm?.UnregisterBlock();
        Destroy(gameObject, 0.1f);
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

    void spawnPowerupIceFlower()
    {
        if (IceFlowerPrefab != null)
        {
            Vector3 spawnPosition = transform.position;
            Instantiate(IceFlowerPrefab, spawnPosition, Quaternion.identity);
        }
        else 
        {
            Debug.LogError("IceFlower no se ha asignado.");
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

    void spawnPlusClock()
    {
        if (PlusClockPrefab != null)
        {
            Vector3 spawnPosition = transform.position;
            Instantiate(PlusClockPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("PlusClock no se ha asignado.");
        }
    }

    void spawnMinusClock()
    {
        if (MinusClockPrefab != null)
        {
            Vector3 spawnPosition = transform.position;
            Instantiate(MinusClockPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("MinusClock no se ha asignado.");
        }
    }

    void spawnStar()
    {
        if (StarPrefab != null)
        {
            Vector3 spawnPosition = transform.position;
            Instantiate(StarPrefab, spawnPosition, Quaternion.identity);
        }
        else
        {
            Debug.LogError("Star no se ha asignado.");
        }
    }

    void Update()
    {

    }


}
