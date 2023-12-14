using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionDetection : MonoBehaviour
{
    private Enemy enemyScript;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyScript = GameObject.Find("Enemy").GetComponent<Enemy>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    //Destroys the enemy game objects when they collide with the player
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Player")) 
        {
            Destroy(gameObject);
        }
    } 
}
