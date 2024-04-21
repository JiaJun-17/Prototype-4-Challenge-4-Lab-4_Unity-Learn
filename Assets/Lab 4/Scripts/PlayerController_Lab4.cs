using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController_Lab3 : MonoBehaviour
{
    private float speed = 100.0f;
    private float zUpperBoundary = 5.5f;
    private float zLowerBoundary = -6.0f;
    private Rigidbody playerRigidBody;     //because I using RigidBody component to control player's movement


    // Start is called before the first frame update
    void Start()
    {
        playerRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        MovePlayer();  //clean code
        PlayerMovementConstraint();  //clean code
    }

    //Move player method
    //Move the player based on arrow key pressed
    void MovePlayer(){
        //player's input
        float horizontalInput = Input.GetAxis("Horizontal");
        float verticalInput = Input.GetAxis("Vertical");

        //add forces to the object
        playerRigidBody.AddForce(Vector3.forward * speed * verticalInput);  //using add force instead of translate because I use rigidbody 
        playerRigidBody.AddForce(Vector3.right * speed * horizontalInput);
    }

    //Player movement constraint method
    //Prevent the player from leaving the top and bottom of the screen
    void PlayerMovementConstraint(){
        //player cannot exceed the bottom of the screen
        if(transform.position.z < zLowerBoundary){
            transform.position = new Vector3(transform.position.x , transform.position.y , zLowerBoundary);
        }

        //player cannot exceed the top of the screen
        if(transform.position.z > zUpperBoundary){
            transform.position = new Vector3(transform.position.x , transform.position.y , zUpperBoundary);
        }
    }

    private void OnCollisionEnter(Collision collision){
        if(collision.gameObject.CompareTag("Enemy")){
            Debug.Log("Player has collided with enemy");
        }
    }

    private void OnTriggerEnter(Collider other){
        if(other.gameObject.CompareTag("Power Up")){
            Destroy(other.gameObject);
        }
    }
}
