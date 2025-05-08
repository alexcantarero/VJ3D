using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUpBehaviour : MonoBehaviour
{

    
    
    public float rotationSpeed = 100f;
    //Vector3 direction = new Vector3(0, 0, -1);
    public float speed = 3f;


    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);

        transform.position += Vector3.back * speed * Time.deltaTime;

        if (transform.position.z < -18.0f) Destroy(gameObject); //Se sale de los límites
        

            
        

    }
}
