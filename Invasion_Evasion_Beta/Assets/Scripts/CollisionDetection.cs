using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private Enemy enemyScript;
    
    void Start()
    {
        enemyScript = GameObject.Find("Enemy").GetComponent<Enemy>();
    }

    void Update()
    {
        
    }
    //Destroys the enemy game objects when they collide with the player
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            //Destroy if collide with player
            Destroy(gameObject);
        }
    } 
}
