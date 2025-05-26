using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThwompBehaviour : MonoBehaviour
{

    int life = 3;

   
    private Rigidbody rb;

    public bool grounded = false; // Variable to check if the Thwomp is grounded

    public ParticleSystem dieEffect;


    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shell") || collision.gameObject.CompareTag("BulletBill"))
        {
            // Assuming the player has a method to handle being hit
            life--;
            dieEffect.Play();
            Debug.Log("Thwomp got hit");
            if (life <= 0)
            {
                Debug.Log("Thwomp defeated");
                StartCoroutine(DieWithEffect());
            }
        }
        else if (collision.gameObject.CompareTag("Ground") || collision.gameObject.CompareTag("Block"))
        {
            grounded = true;
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
        Destroy(gameObject);
    }

    private void Jump()
    {
        grounded = false;
        rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
        //yield return new WaitForSeconds(3f);

    }

    // Update is called once per frame
    void Update()
    {
        if(grounded) Jump();

    }
}
