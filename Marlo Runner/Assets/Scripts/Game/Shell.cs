using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shell : MonoBehaviour {

    public float rotatingSpeed = 180f;
    public float movementSpeed = 10f;
    public float radiusTolerance = 0.4f;

    private bool movingRight = true;
    
    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
        transform.RotateAround(transform.position, Vector3.up, rotatingSpeed * Time.deltaTime);

        transform.position = new Vector3(
            transform.position.x + movementSpeed * (movingRight ? 1 : -1) * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );

    }

    void OnTriggerEnter(Collider other) {

        if(other.GetComponent<Enemy>() != null) {
            Destroy(other.gameObject);
        }
        else if(other.GetComponent<Destroyer>() != null ||
                other.tag == "JumpingArea") {
            return;
        }
        else if(transform.position.y < other.transform.position.y + radiusTolerance) {
            if(transform.position.x < other.transform.position.x && movingRight) {
                movingRight = false;
            }
            else if(transform.position.x > other.transform.position.x && !movingRight) {
                movingRight = true;
            }
        }

    }

    void OnCollisionEnter(Collision other) {

        if(other.gameObject.GetComponent<Player>() != null) {
            return;
        }

        if(transform.position.y < other.transform.position.y + radiusTolerance) {
            if(transform.position.x < other.transform.position.x && movingRight) {
                movingRight = false;
            }
            else if(transform.position.x > other.transform.position.x && !movingRight) {
                movingRight = true;
            }
        }
        
    }

}
