using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainChompBehaviour : MonoBehaviour
{

    int life = 3;

    private Animator animator;

    public ParticleSystem dieEffect;


    void Start()
    {
        animator = GetComponent<Animator>();
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
        
    }
}
