using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float movementSpeed = 4f;
    public float acceleration = 2.5f;
    public float jumpingSpeed = 6f;

    private float speed = 0f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        speed += acceleration * Time.deltaTime;
        if(speed > movementSpeed) {
            speed = movementSpeed;
        }

        GetComponent<Rigidbody>().velocity = new Vector3(
            speed,
            GetComponent<Rigidbody>().velocity.y,
            GetComponent<Rigidbody>().velocity.z
        );

        if(Input.GetMouseButtonDown(0) || Input.GetKey("space")) {
            GetComponent<Rigidbody>().velocity = new Vector3(
                GetComponent<Rigidbody>().velocity.x,
                jumpingSpeed,
                GetComponent<Rigidbody>().velocity.z
            );
        }

    }

}
