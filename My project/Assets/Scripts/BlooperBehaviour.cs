using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlooperBehaviour : MonoBehaviour
{

    int life = 3;


    private Rigidbody rb;

    public bool grounded = false; // Variable to check if the Thwomp is grounded

    public ParticleSystem dieEffect;

    public AudioManager audioManager;
    private float whompTimer = 0f;
    private float whompInterval = 5f;



    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioManager = GameObject.FindGameObjectWithTag("AudioManager").GetComponent<AudioManager>();
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


    // Update is called once per frame
    void Update()
    {
        {
            whompTimer += Time.deltaTime;
            if (whompTimer >= whompInterval)
            {
                rb.AddForce(Vector3.up * 10f, ForceMode.Impulse);
                whompTimer = 0f;
            }
        }

    }
}
