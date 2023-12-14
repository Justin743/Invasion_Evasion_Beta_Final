using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOutOfBounds : MonoBehaviour
{
    private float bounds = 30;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Destroy projectile if it goes past 30
        if (Mathf.Abs(transform.position.z) > bounds || Mathf.Abs(transform.position.x) > bounds)
        {
            Destroy(gameObject);
        }
    }
}
