using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChainChompBehaviour : MonoBehaviour
{

    int life = 3;

    private Animator animator;


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
            Debug.Log("ChainChomp got hit");
            if (life <= 0)
            {
                Debug.Log("ChainChomp defeated");
                Destroy(gameObject);
            }
        }
        else if (collision.gameObject.CompareTag("Ground"))
        { 
            animator.SetBool("isGrounded", true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
