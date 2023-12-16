using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossBehaviour : MonoBehaviour
{
    private Rigidbody enemyRB;
    private GameObject player;
    private UIManager uiManagerScript;

    //Particle for the boss
    public ParticleSystem explosionParticle;

    //Sounds for the boss
    private AudioSource bossAudio;
    public AudioClip explosionSound;

    public float speed = 2.0f;
    public int health = 10;
    // Start is called before the first frame update
    void Start()
    {
        enemyRB = GetComponent<Rigidbody>();
        player = GameObject.Find("Player");
        //Finds the UI manager script 
        uiManagerScript = GameObject.Find("UI Manager").GetComponent<UIManager>();
        bossAudio = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.LookAt(player.transform);
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
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
                explosionParticle.Play();
                bossAudio.PlayOneShot(explosionSound, 1.0f);
                StartCoroutine(destroyEnemyCountDown());
            }

            //Destroy the bullet when it hits the enemy
            Destroy(other.gameObject);
        }
    }

    IEnumerator destroyEnemyCountDown()
    {
        yield return new WaitForSeconds(0.2f);
        Destroy(gameObject);
        uiManagerScript.UpdateScore(25);
    }
}
