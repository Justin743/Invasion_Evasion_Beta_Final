using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    private Rigidbody enemyRB;
    private GameObject player;
    private UIManager uiManagerScript;

    public float speed = 2.0f;
    public int health = 10;
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
        Vector3 lookDirection = (player.transform.position - transform.position).normalized;
        enemyRB.AddForce(lookDirection * speed);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Bullet"))
        {
            //Decrease boss health when hit by a bullet
            health--;

            //if health = 0
            if (health <= 0)
            {
                //Destroy the boss when it health = 0
                Destroy(gameObject);
                //update score when boss is destroyed
                uiManagerScript.UpdateScore(25);
            }

            //Destroy the bullet when it hits the enemy
            Destroy(other.gameObject);
        }
    }
}
