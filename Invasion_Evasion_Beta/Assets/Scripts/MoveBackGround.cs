using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBackGround : MonoBehaviour
{
    private float speed = 15;
    void Start()
    {
        
    }

    void Update()
    {
        //moves background to the left based on speed
        transform.Translate(Vector3.left * Time.deltaTime * speed);
    }
}
