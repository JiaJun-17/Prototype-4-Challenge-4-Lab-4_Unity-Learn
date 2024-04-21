using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDown_Lab4 : MonoBehaviour
{
    public float speed = 5.0f;  //different enemy has different speed
    private Rigidbody objectRigidBody;
    private float zDestroy = -8.0f;

    // Start is called before the first frame update
    void Start()
    {
        objectRigidBody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        objectRigidBody.AddForce(Vector3.forward * -speed); //AddForce() because we using RigidBoby

        if(transform.position.z < zDestroy){
            Destroy(gameObject);
        }
    }
}
