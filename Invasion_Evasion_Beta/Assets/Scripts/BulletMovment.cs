using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovment : MonoBehaviour
{
    private float speed = 40.0f;
    private float bulletLife = 4.0f;

    void Start()
    {
        
    }
    void Update()
    {
        //Bullet movment
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        //Destroy itself on collision
        Destroy(collision.gameObject);
        Destroy(gameObject);
    }

    private void Awake()
    {
        //Destroys itself after awhile of not hitting anything
        Destroy(gameObject, bulletLife);
    }
}
