using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float bounds = 30;

    void Start()
    {
        
    }

    void Update()
    {
        //Destroy projectile if it goes past 30
        if (Mathf.Abs(transform.position.z) > bounds || Mathf.Abs(transform.position.x) > bounds)
        {
            Destroy(gameObject);
        }
    }
}
