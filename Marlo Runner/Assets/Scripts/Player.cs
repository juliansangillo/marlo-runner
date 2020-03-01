using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Player : MonoBehaviour {

    [Header("Movement Fields")]
    [Range(4f, 6f)]
    public float movementSpeed = 4f;
    public float movementSpeedRight = 8f;
    public float movementSpeedLeft = 2f;

    [Header("Acceleration")]
    public float acceleration = 2.5f;
    public float deacceleration = 5.0f;

    [Header("Jumping Fields")]
    public float normalJumpingSpeed = 6f;
    public float longJumpingSpeed = 10f;
    public float jumpDuration = 0.75f;
    public float verticalWallJumpingSpeed = 5f;
    public float horizontalWallJumpingSpeed = 3.5f;

    public Action onCollectCoin;

    private float speed = 0f;
    private float jumpingSpeed = 0f;
    private float jumpingTimer = 0f;

    private bool dead = false;
    private bool paused = false;
    private bool canJump = false;
    private bool jumping = false;
    private bool canWallJump = false;
    private bool wallJumpLeft = false;
    private bool onSpeedAreaLeft = false;
    private bool onSpeedAreaRight = false;
    private bool onLongJumpBlock = false;

    public bool Dead {
        get {
            return dead;
        }
    }

    // Start is called before the first frame update
    void Start() {
        jumpingSpeed = normalJumpingSpeed;
    }

    // Update is called once per frame
    void Update() {

        if(dead) {
            return;
        }

        speed += acceleration * Time.deltaTime;

        float targetMovementSpeed = movementSpeed;
        if(onSpeedAreaLeft) {
            targetMovementSpeed = movementSpeedLeft;
        }
        else if(onSpeedAreaRight) {
            targetMovementSpeed = movementSpeedRight;
        }

        if(speed > targetMovementSpeed) {
            speed -= deacceleration * Time.deltaTime;
        }

        GetComponent<Rigidbody>().velocity = new Vector3(
            paused ? 0 : speed,
            GetComponent<Rigidbody>().velocity.y,
            GetComponent<Rigidbody>().velocity.z
        );

        bool pressingJumpButton = Input.GetMouseButton(0) || Input.GetKey("space");
        if(pressingJumpButton) {
            if(canJump) {
                Jump();
            }
        }

        if(paused && pressingJumpButton) {
            paused = false;
        }

        if(jumping) {
            jumpingTimer += Time.deltaTime;

            if(pressingJumpButton && jumpingTimer < jumpDuration) {
                if(onLongJumpBlock) {
                    jumpingSpeed = longJumpingSpeed;
                }

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

        if(trig.GetComponent<LongJumpBlock>() != null) {
            onLongJumpBlock = true;
        }

        if(trig.GetComponent<Enemy>() != null) {
            Enemy enemy = trig.GetComponent<Enemy>();
            if(enemy.Dead == false) {
                Kill();
            }
        }

    }

    void OnTriggerStay(Collider trig) {

        if(trig.tag == "JumpingArea") {
            canJump = true;
            jumping = false;
            jumpingSpeed = normalJumpingSpeed;
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

        if(trig.GetComponent<LongJumpBlock>() != null) {
            onLongJumpBlock = false;
        }

    }

    void Kill() {

        dead = true;
        GetComponent<BoxCollider>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 500f, -800f));

    }

    public void Jump(bool forced = false) {
        jumping = true;

        if(forced) {
            GetComponent<Rigidbody>().velocity = new Vector3(
                GetComponent<Rigidbody>().velocity.x,
                jumpingSpeed,
                GetComponent<Rigidbody>().velocity.z
            );
        }
    }

}
