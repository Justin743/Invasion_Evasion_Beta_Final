using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    private Camera mainCam;
    public GameObject projectilePrefab;
    public UIManager uiManagerScript;


    //Movement and bounds
    public float horizontalInput;
    public float verticalInput;
    private float speed = 10.0f;
    private float xRange = 14;
    private float zRange = 7.0f;

    //Projectile
    public float projectileSpeed = 20.0f;

    //Power up
    public bool hasPowerup;
    public float powerupDuration = 15.0f;

    //Players health
    public int health = 3;


    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        mainCam = FindObjectOfType<Camera>();
        uiManagerScript = GameObject.Find("UI Manager").GetComponent<UIManager>();
        

    }
    void Update()
    {
        //Movement of the player
        horizontalInput = Input.GetAxis("Horizontal");
        verticalInput = Input.GetAxis("Vertical");
        transform.Translate(Vector3.right * horizontalInput * Time.deltaTime * speed);
        transform.Translate(Vector3.forward * verticalInput * Time.deltaTime * speed);

        uiManagerScript.UpdateHealth(health);

        boundaries();
        HandleMouseLook();

        //If space pressed then shoot projectile.
        if (Input.GetKeyDown(KeyCode.Space))
        {
 
            // Instantiate the projectile from the player's position and in the direction the player is facing
            GameObject projectile = Instantiate(projectilePrefab, transform.position, transform.rotation);

            // Apply force to the projectile in the forward direction of the player
            Rigidbody projectileRb = projectile.GetComponent<Rigidbody>();
            projectileRb.AddForce(transform.forward * projectileSpeed, ForceMode.Impulse);
        }
    }

    void boundaries() {
        //Bounds for x axis
        //IMPORTANT NOTE - " playerRb.velocity = Vector3.zero;" sets players velocity to zero when it hits the boundary.
        //This allows the player to move away from the bounds without having to wait for the velocity to reach zero again.
        if (transform.position.x < -xRange)
        {
            transform.position = new Vector3(-xRange, transform.position.y, transform.position.z);
            playerRb.velocity = Vector3.zero;
        }
        else if (transform.position.x > xRange)
        {
            transform.position = new Vector3(xRange, transform.position.y, transform.position.z);
            playerRb.velocity = Vector3.zero;
        }

        // Bounds for the z axis
        if (transform.position.z < -zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, -zRange);
            playerRb.velocity = Vector3.zero;
        }
        else if (transform.position.z > zRange)
        {
            transform.position = new Vector3(transform.position.x, transform.position.y, zRange);
            playerRb.velocity = Vector3.zero;
        }
    }

    //Follow players mouse pos, SOURCED FROM YOUTUBE (REFERENCE IN WORD DOC).
    void HandleMouseLook()
    {
        Ray cameraRay = mainCam.ScreenPointToRay(Input.mousePosition);
        Plane groundPlane = new Plane(Vector3.up, Vector3.zero);
        float rayLength;

        if (groundPlane.Raycast(cameraRay, out rayLength))
        {
            Vector3 pointToLook = cameraRay.GetPoint(rayLength);
            

            // Rotates player
            transform.LookAt(new Vector3(pointToLook.x, transform.position.y, pointToLook.z));
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //If collide with powerup sets powerup to true
        if (other.CompareTag("Powerup"))
            
        {
            hasPowerup = true;
            //Destroys powerup prefab from scene
            Destroy(other.gameObject);
            //If health is less than 3, add to the health when picking up a powerup
            if (health <3) {
                health++;
            }
            //Starts a countdown of 15 seconds from PowerupCoroutine.
            StartCoroutine(PowerupCountdownRoutine());
            
        }
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        //if you collide with gameobject of tag "Enemy"
        if (collision.gameObject.CompareTag("Enemy"))
            
        {
            //take 1 from the health
            health--;

            //if health = 0
            if (health <= 0)
            {
                //destroy game object
                Destroy(gameObject);
                uiManagerScript.gameOver();
            }
           
        }

        if (collision.gameObject.CompareTag("Boss"))
        {
            //take 1 from the health
            health = 0;

            //if health = 0
            
                //destroy game object
                Destroy(gameObject);
                uiManagerScript.gameOver();
            
        }
    }

    //IENUMERATOR
    IEnumerator PowerupCountdownRoutine() {
        //waits for 15 seconds and sets powerup to false.
        yield return new WaitForSeconds(15);
        hasPowerup = false;
    }
}
