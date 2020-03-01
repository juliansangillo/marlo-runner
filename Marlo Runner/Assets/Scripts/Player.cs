using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour {

    public float movementSpeed = 4f;
    public float acceleration = 2.5f;
    public float jumpingSpeed = 6f;
    public float jumpDuration = 0.75f;
    public float verticalWallJumpingSpeed = 5f;
    public float horizontalWallJumpingSpeed = 3.5f;

    public Action onCollectCoin;

    private float speed = 0f;
    private float jumpingTimer = 0f;

    private bool paused = false;
    private bool canJump = false;
    private bool jumping = false;
    private bool canWallJump = false;
    private bool wallJumpLeft = false;
    private bool onSpeedAreaLeft = false;
    private bool onSpeedAreaRight = false;

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
            paused ? 0 : speed,
            GetComponent<Rigidbody>().velocity.y,
            GetComponent<Rigidbody>().velocity.z
        );

        bool pressingJumpButton = Input.GetMouseButton(0) || Input.GetKey("space");
        if(pressingJumpButton) {
            if(canJump) {
                jumping = true;
            }
        }

        if(paused && pressingJumpButton) {
            paused = false;
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

    public void Pause() {
        paused = true;
    }

    void OnTriggerEnter(Collider trig) {
        
        if(trig.transform.GetComponent<Coin>() != null) {
            Destroy(trig.gameObject);
            onCollectCoin();
        }

        SpeedArea speedArea = trig.GetComponent<SpeedArea>();
        if(speedArea != null) {
            if(speedArea.direction == Direction.Left) {
                onSpeedAreaLeft = true;
            }
            else if(speedArea.direction == Direction.Right) {
                onSpeedAreaRight = true;
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

        SpeedArea speedArea = trig.GetComponent<SpeedArea>();
        if(speedArea != null) {
            if(speedArea.direction == Direction.Left) {
                onSpeedAreaLeft = false;
            }
            else if(speedArea.direction == Direction.Right) {
                onSpeedAreaRight = false;
            }
        }

    }

}
