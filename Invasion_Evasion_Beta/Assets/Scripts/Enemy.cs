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

    //Particle for the enemy
    public ParticleSystem explosionParticle;

    //Sounds for the enemy
    private AudioSource enemyAudio;
    public AudioClip explosionSound;

    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        //Finds the UI manager script 
        uiManagerScript = GameObject.Find("UI Manager").GetComponent<UIManager>();
        enemyAudio = GetComponent<AudioSource>();


    }

    // Update is called once per frame
    void Update()
    {
        
        //Enemy movment
       // Vector3 lookDirection = (player.transform.position - transform.position).normalized;

        transform.LookAt(player.transform);

        transform.Translate(Vector3.forward * speed * Time.deltaTime);

        

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
                explosionParticle.Play();
                enemyAudio.PlayOneShot(explosionSound, 1.0f);
                StartCoroutine(destroyEnemyCountDown());
            }

            //Destroy the bullet when it hits the enemy
            Destroy(other.gameObject);
        }
    }

    IEnumerator destroyEnemyCountDown()
    {
        //waits for 15 seconds and sets powerup to false.
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        uiManagerScript.UpdateScore(5);
    }
}