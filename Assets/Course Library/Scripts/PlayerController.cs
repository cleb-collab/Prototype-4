using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRb;
    public float speed = 5.0f;
    private GameObject focalPoint;
    private float fowardInput;
    public bool hasPowerup;
    private float powerupStrength = 25.0f;
    public GameObject powerupIndicator;

    public float hangTime;
    public float smashSpeed;
    public float explosionForce;
    public float explosionRadius;
    bool smashing = false;
    float floorY;
    // Start is called before the first frame update
    void Start()
    {
        focalPoint = GameObject.Find("Focal Point");
        playerRb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        fowardInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * speed * fowardInput);
        powerupIndicator.transform.position = transform.position + new Vector3(0,-0.5f, 0); 

       
    }
    
    private void OnTriggerEnter(Collider other) {
        if (other.CompareTag("Powerup")) {
            hasPowerup = true;
            Destroy(other.gameObject);
            StartCoroutine(PowerupCountdownRoutine());
            powerupIndicator.gameObject.SetActive(true);

        }
    }
    private void OnCollisionEnter(Collision collision) {
        if (collision.gameObject.CompareTag("Enemy") && hasPowerup) {
            Rigidbody enemyRigidbody = collision.gameObject.GetComponent<Rigidbody>();   
            Vector3 awayFromPlayer = (collision.gameObject.transform.position - transform.position);

            Debug.Log("Collide with" + collision.gameObject.name + "with powerup set to" + hasPowerup);
            enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
        }
    }
    IEnumerator PowerupCountdownRoutine() {
        yield return new WaitForSeconds(7);
        hasPowerup = false;
        powerupIndicator.gameObject.SetActive(false);
    }

    IEnumerator Smash()
    {
        var enemies = FindObjectsOfType<Enemy>();
        //Store the y position before taking off
        floorY = transform.position.y;
        //Calculate the amount of time we will go up
        float jumpTime = Time.time + hangTime;
        while(Time.time < jumpTime)
        {
        //move the player up while still keeping their x velocity.
        playerRb.velocity = new Vector2(playerRb.velocity.x, smashSpeed);
        yield return null;
        }
        //Now move the player down
        while(transform.position.y > floorY)
        {
        playerRb.velocity = new Vector2(playerRb.velocity.x, -smashSpeed * 2);
        yield return null;
        }
        //Cycle through all enemies.
    for (int i = 0; i < enemies.Length; i++)
    {
    //Apply an explosion force that originates from our position.
    if(enemies[i] != null)
    enemies[i].GetComponent<Rigidbody>().AddExplosionForce(explosionForce,

    transform.position, explosionRadius, 0.0f, ForceMode.Impulse);
    }
    //We are no longer smashing, so set the boolean to false
    smashing = false;
        }

}
