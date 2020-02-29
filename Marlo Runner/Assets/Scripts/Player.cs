using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float movementSpeed = 4f;
    public float acceleration = 2.5f;
    public float jumpingSpeed = 6f;
    public float jumpDuration = 0.75f;

    private float speed = 0f;
    private float jumpingTimer = 0f;

    private bool canJump = false;
    private bool jumping = false;

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

        bool pressingJumpButton = Input.GetMouseButton(0) || Input.GetKey("space");
        if(pressingJumpButton) {
            if(canJump) {
                jumping = true;
            }
        }

        if(jumping) {
            jumpingTimer += Time.deltaTime;

            if(pressingJumpButton && jumpingTimer < jumpDuration) {
                GetComponent<Rigidbody>().velocity = new Vector3(
                    GetComponent<Rigidbody>().velocity.x,
                    jumpingSpeed,
                    GetComponent<Rigidbody>().velocity.z
                );
            }
        }

    }

    void OnTriggerStay(Collider trig) {
        if(trig.tag == "JumpingArea") {
            canJump = true;
            jumping = false;
            jumpingTimer = 0f;
        }
    }

}
