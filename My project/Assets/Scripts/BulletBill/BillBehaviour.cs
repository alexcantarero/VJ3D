using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BillBehaviour : MonoBehaviour
{
    public float speed = 15f;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;


        if(transform.position.z >= 47.34) Destroy(gameObject); //Se sale de los límites
    }
}
