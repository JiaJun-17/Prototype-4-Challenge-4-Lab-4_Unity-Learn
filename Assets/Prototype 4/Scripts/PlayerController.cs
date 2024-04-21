using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;


public class PlayerController : MonoBehaviour
{
    private Rigidbody playerRigidBody;
    private GameObject focalPoint; //because focal point is a game object
    private float powerUpStrength = 15.0f;
    public float speed  = 5.0f;
    public bool hasPowerUp = false;
    public GameObject powerUpIndicator; //create reference to powerup indicator //public so that can drag from the hierarchy and drop to the variable I want

    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>(); //get reference
        focalPoint = GameObject.Find("Focal Point"); //get reference to the Focal Point in the hierarchy
    }

    // Update is called once per frame
    void Update()
    {   
        //get input for up/down arrow key
        float forwardInput = Input.GetAxis("Vertical");
        //use the local direction of the focal point to move the player in that direction
        playerRigidBody.AddForce(focalPoint.transform.forward * forwardInput * speed);

        //set the power up indicator's position to the player's position
        //the powerUpIndicator will follow player
        //why = transform.posisiton? coz it is player controller script that applied under player
        powerUpIndicator.transform.position = transform.position + new Vector3(0, -0.6f, 0); // + offset
    }

    private void OnTriggerEnter (Collider other){   //trigger between colliders

        //if run into the Pawer Up object
        if(other.CompareTag("Power Up")){
            hasPowerUp = true; //has powerup
            powerUpIndicator.gameObject.SetActive(true); //because hasPowerUp=true, so set active to true
            Destroy(other.gameObject); 
            StartCoroutine(PowerUpCountDownRoutine());  //start the timer, the powerup only last for 7 secs
        }
    }

    //countdown timer
    IEnumerator PowerUpCountDownRoutine(){
        yield return new WaitForSeconds(7); //wait for 7 secs then player has no powerup
        hasPowerUp = false; //reset to before player pick up the powerup
        powerUpIndicator.gameObject.SetActive(false); //because hasPowerUp=false, so set active to false
    }

    private void OnCollisionEnter (Collision collision){

        if(collision.gameObject.CompareTag("Enemy") && hasPowerUp){
            //local variable    //get the rigidbody of the enemy
            Rigidbody enemyRigidBody = collision.gameObject.GetComponent<Rigidbody>();  
            Vector3 awayFromPlayer = collision.gameObject.transform.position - transform.position; //enemy's current position-player's current position, to send the enemy away from the player

            //Apply force to the enemy
            enemyRigidBody.AddForce(awayFromPlayer * powerUpStrength, ForceMode.Impulse);
            Debug.Log("Collided with " + collision.gameObject.name + " with powerup set to " + hasPowerUp);
        }

    }
}
