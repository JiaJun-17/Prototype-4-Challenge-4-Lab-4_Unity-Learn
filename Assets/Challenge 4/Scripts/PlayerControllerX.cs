using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControllerX : MonoBehaviour
{
    private Rigidbody playerRb;
    private float speed = 500;
    private GameObject focalPoint;

    public bool hasPowerup;
    public bool hasTurboBoost;
    public GameObject powerupIndicator;
    public GameObject turboBoostIndicator;
    public int powerUpDuration = 5;
    public int turboBoostDuration = 4;

    private float normalStrength = 5; // how hard to hit enemy without powerup
    private float powerupStrength = 25; // how hard to hit enemy with powerup
    private float boostForce = 10.0f;

    void Start()
    {
        playerRb = GetComponent<Rigidbody>();
        focalPoint = GameObject.Find("Focal Point");
    }

    void Update()
    {
        // Add force to player in direction of the focal point (and camera)
        float verticalInput = Input.GetAxis("Vertical");
        playerRb.AddForce(focalPoint.transform.forward * verticalInput * speed * Time.deltaTime); 

        // Set powerup indicator position to beneath player
        powerupIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0);

        // Set turbo boost indicator position to beneath player
        turboBoostIndicator.transform.position = transform.position + new Vector3(0, 1.5f, 0);

        //boost force added to player when space bar is pressed
        TurboBoostForce();
    }

    // If Player collides with powerup, activate powerup
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Powerup"))
        {
            Destroy(other.gameObject);
            hasPowerup = true;
            powerupIndicator.SetActive(true);
            StartCoroutine(PowerupCooldown());
        }
    }

    // Coroutine to count down powerup duration
    IEnumerator PowerupCooldown()
    {
        yield return new WaitForSeconds(powerUpDuration);
        hasPowerup = false;
        powerupIndicator.SetActive(false);
    }

    // Coroutine to count down turbo boost duration
    IEnumerator turboBoostCooldown()
    {
        yield return new WaitForSeconds(turboBoostDuration);
        hasTurboBoost = false;
        turboBoostIndicator.SetActive(false);
    }

    // If Player collides with enemy
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            Rigidbody enemyRigidbody = other.gameObject.GetComponent<Rigidbody>();
            Vector3 awayFromPlayer = other.gameObject.transform.position - transform.position; 
           
            if (hasPowerup) // if have powerup hit enemy with powerup force
            {
                enemyRigidbody.AddForce(awayFromPlayer * powerupStrength, ForceMode.Impulse);
            }
            else // if no powerup, hit enemy with normal strength 
            {
                enemyRigidbody.AddForce(awayFromPlayer * normalStrength, ForceMode.Impulse);
            }
        }
    }

    private void TurboBoostForce(){
        if(Input.GetKeyDown(KeyCode.Space)){
            Rigidbody playerRB = GetComponent<Rigidbody>();
            playerRB.AddForce(focalPoint.transform.forward * boostForce , ForceMode.Impulse);
            turboBoostIndicator.SetActive(true);
            StartCoroutine(turboBoostCooldown());
        }
    }
    
}
