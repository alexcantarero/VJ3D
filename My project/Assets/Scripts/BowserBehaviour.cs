using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BowserBehaviour : MonoBehaviour
{

    int life = 3;

    private Animator animator;

    public ParticleSystem dieEffect;
    private Rigidbody rb;

    public AudioManager audioManager;
    private float chompTimer = 0f;
    private float chompInterval = 5f; // Intervalo entre sonidos de mordisco



    void Start()
    {
        animator = GetComponent<Animator>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
        rb = GetComponent<Rigidbody>();
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
        else if (collision.gameObject.CompareTag("Ground"))
        {
            animator.applyRootMotion = true; 
            animator.SetBool("isGrounded", true);
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
        
            audioManager.PlaySFX(audioManager.chainChompSFX);
            rb.AddForce(Vector3.up * 7f, ForceMode.Impulse);
            chompTimer = 0f;
        }
    }
}
