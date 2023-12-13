using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //importing UImanager for score
    private UIManager uiManagerScript;
    public float speed = 2.0f;
    public int health = 2;
    private Rigidbody enemyRB;
    private GameObject player;
    
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        //Finds the UI manager script 
        uiManagerScript = GameObject.Find("UI Manager").GetComponent<UIManager>();
    }

    // Update is called once per frame
    void Update()
    {
        //Enemy movment
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRB.AddForce(lookDirection * speed);

    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //Decrease enemy health when hit by a bullet
            health--;

            //if health = 0
            if (health <= 0)
            {
                //Destroy the enemy when it health = 0
                Destroy(gameObject);
                //update score when enemy is destroyed
                uiManagerScript.UpdateScore(5);
            }

            //Destroy the bullet when it hits the enemy
            Destroy(other.gameObject);
        }
    }
}