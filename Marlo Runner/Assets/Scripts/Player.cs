using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {

    public float movementSpeed = 4f;
    public float acceleration = 2.5f;
    public float jumpingSpeed = 6f;
    public float jumpDuration = 0.75f;
    public float verticalWallJumpingSpeed = 5f;
    public float horizontalWallJumpingSpeed = 3.5f;

    private float speed = 0f;
    private float jumpingTimer = 0f;

    private bool canJump = false;
    private bool jumping = false;
    private bool canWallJump = false;
    private bool wallJumpLeft = false;

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

        if(canWallJump) {
            speed = 0;

            if(pressingJumpButton) {
                canWallJump = false;
                
                speed = wallJumpLeft ? -horizontalWallJumpingSpeed : horizontalWallJumpingSpeed;

                GetComponent<Rigidbody>().velocity = new Vector3(
                    GetComponent<Rigidbody>().velocity.x,
                    verticalWallJumpingSpeed,
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
        else if(trig.tag == "WallJumpingArea") {
            canWallJump = true;
            wallJumpLeft = transform.position.x < trig.transform.position.x;
        }

    }

    void OnTriggerExit(Collider trig) {

        if(trig.tag == "WallJumpingArea") {
            canWallJump = false;
        }

    }

}
