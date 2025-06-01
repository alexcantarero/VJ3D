using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowserBehaviour : MonoBehaviour
{

    int life = 3;

    public ParticleSystem dieEffect;
    public GameObject flamethrower;
    private Rigidbody rb;

    public AudioManager audioManager;
    private float chompTimer = 0f;
    private float chompInterval = 3f; 



    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody>();
        flamethrower.transform.Rotate(0, 0, -117.352f); // Asegúrate de que el lanzallamas esté orientado correctamente

    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shell") || collision.gameObject.CompareTag("BulletBill"))
        {
            // Assuming the player has a method to handle being hit
            life--;
            dieEffect.Play();
            Debug.Log("ChainChomp got hit");
            if (life <= 0)
            {
                Debug.Log("ChainChomp defeated");
                StartCoroutine(DieWithEffect());
            }
        }
        
    }

    private IEnumerator DieWithEffect()
    {
        if (dieEffect != null)
        {
            dieEffect.transform.parent = null;
            dieEffect.Play();
            yield return new WaitForSeconds(dieEffect.main.duration);
            //Destroy(dieEffect.gameObject); // Destruye el sistema de partículas después
        }

        GameManager gm;
        GameObject gameManager = GameObject.Find("GameManager");
        gm = gameManager.GetComponent<GameManager>();
        gm?.EnemyDead();

        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        chompTimer += Time.deltaTime;

        if (chompTimer >= chompInterval) { 
        
            audioManager.PlaySFX(audioManager.bowserSFX);
            StartCoroutine(FireCoroutine());
            rb.AddForce(Vector3.up * 7f, ForceMode.Impulse);
            chompTimer = 0f;
        }
    }

    IEnumerator FireCoroutine()
    {
        Debug.Log("hola");
        flamethrower.SetActive(true);
        yield return new WaitForSeconds(2.5f); // Duración del lanzallamas
        flamethrower.SetActive(false);
    }
}
