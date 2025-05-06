using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockBehaviour : MonoBehaviour
{

    void Start()
    {
        
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Shell"))
        {
            Destroy(gameObject, 0.1f); //Destrucción con delay
        }
    }

    void Update()
    {

    }


}
