using System;
using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour {

    [Header("Visuals")]
    [SerializeField] private GameObject model = null;
    [SerializeField] private GameObject normalModel = null;
    [SerializeField] private GameObject powerUpModel = null;

    [Header("Movement Fields")]
    [SerializeField] [Range(1f, 15f)] private float movementSpeed = 0;
    [SerializeField] [Range(1f, 15f)] private float movementSpeedRight = 0;
    [SerializeField] [Range(1f, 15f)] private float movementSpeedLeft = 0;

    [Header("Acceleration")]
    [SerializeField] private float acceleration = 0;
    [SerializeField] private float deacceleration = 0;

    [Header("Jumping Fields")]
    [SerializeField] private float normalJumpingSpeed = 0;
    [SerializeField] private float longJumpingSpeed = 0;
    [SerializeField] private float destroyEnemyJumpingSpeed = 0;
    [SerializeField] private float jumpDuration = 0;
    [SerializeField] private float verticalWallJumpingSpeed = 0;
    [SerializeField] private float horizontalWallJumpingSpeed = 0;
    [SerializeField] private float horizontalBounceAfterHit = 0;
    [SerializeField] private float verticalBounceAfterHit = 0;

    [Header("Power ups")]
    [SerializeField] private int maxLives = 0;
    [SerializeField] private float invincibilityDuration = 0;

    private IInfo playerData;

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
    private bool finished = false;

    private bool hasPowerUp = false;
    private bool hasInvincibility = false;

    private Action onCollectCoin;

    public bool Dead {
        get {
            return dead;
        }
    }

    public bool Finished {
        get {
            return finished;
        }
    }

    public Action OnCollectCoin {
        get {
            return onCollectCoin;
        }
        set {
            onCollectCoin = value;
        }
    }

    public void Pause() {

        paused = true;

    }

    public void Jump(bool hitEnemy = false) {
        
        if(!finished)
            jumping = true;

        if(hitEnemy) {
            Rigidbody body = GetComponent<Rigidbody>();

            body.velocity = new Vector3(
                body.velocity.x,
                destroyEnemyJumpingSpeed,
                body.velocity.z
            );
        }

    }

    public void OnDestroyBrick() {

        GetComponent<Rigidbody>().velocity = new Vector3(
            GetComponent<Rigidbody>().velocity.x,
            0,
            GetComponent<Rigidbody>().velocity.z
        );
        canJump = false;
        jumping = false;

    }

    private void Start() {
        
        jumpingSpeed = normalJumpingSpeed;

        normalModel.SetActive(true);
        powerUpModel.SetActive(false);

        playerData = GetComponent<InfoObjectControl>().InfoObj.GetInfo();
        if(!playerData.Exists("lives") && !playerData.Exists("maxLives")) {
            playerData["lives"] = maxLives;
            playerData["maxLives"] = maxLives;
        }

    }

    private void Update() {

        if(dead) {
            return;
        }

        move();

        bool pressingJumpButton = Input.GetMouseButton(0) || Input.GetKey("space");
        if(pressingJumpButton) {
            if(canJump)
                Jump();
        }

        if(jumping)
            OnJumpIf(pressingJumpButton);
        
        //Debug.Log("Can Jump: " + canJump);
        //Debug.Log("Jumping: " + jumping);

        if(canWallJump) {
            speed = 0;

            if(pressingJumpButton)
                wallJump();
        }

    }

    private void move() {

        float targetMovementSpeed;
        Rigidbody body = GetComponent<Rigidbody>();

        targetMovementSpeed = movementSpeed;
        if(onSpeedAreaLeft) {
            targetMovementSpeed = movementSpeedLeft;
        }
        else if(onSpeedAreaRight) {
            targetMovementSpeed = movementSpeedRight;
        }

        if(speed > targetMovementSpeed) {
            speed -= deacceleration * Time.deltaTime;
        }
        else {
            speed += acceleration * Time.deltaTime;
        }

        body.velocity = new Vector3(
            paused ? 0 : speed,
            body.velocity.y,
            body.velocity.z
        );

    }

    private void OnJumpIf(bool pressingJumpButton) {

        Rigidbody body = GetComponent<Rigidbody>();

        if(paused) {
            paused = false;
        }

        jumpingTimer += Time.deltaTime;

        if(pressingJumpButton && jumpingTimer < jumpDuration) {
            if(onLongJumpBlock) {
                jumpingSpeed = longJumpingSpeed;
            }

            body.velocity = new Vector3(
                body.velocity.x,
                jumpingSpeed,
                body.velocity.z
            );
        }

    }

    private void wallJump() {

        Rigidbody body = GetComponent<Rigidbody>();

        canWallJump = false;
        
        speed = wallJumpLeft ? -horizontalWallJumpingSpeed : horizontalWallJumpingSpeed;

        body.velocity = new Vector3(
            body.velocity.x,
            verticalWallJumpingSpeed,
            body.velocity.z
        );

    }

    private void OnCollisionEnter(Collision other) {

        checkForEnemy(other);
        
    }

    private void OnTriggerEnter(Collider other) {
        
        checkForCoin(other);
        checkForSpeedArea(other);
        checkForPowerUp(other);
        checkForFinishLine(other);

    }

    private void OnTriggerStay(Collider other) {

        if(other.tag == "JumpingArea") {
            canJump = true;
            jumping = false;
            jumpingSpeed = normalJumpingSpeed;
            jumpingTimer = 0f;
        }
        else if(other.tag == "WallJumpingArea") {
            canWallJump = true;
            wallJumpLeft = transform.position.x < other.transform.position.x;
        }

    }

    private void OnTriggerExit(Collider other) {

        if(other.tag == "WallJumpingArea") {
            canWallJump = false;
        }

        checkForExitSpeedArea(other);

    }

    private void checkForCoin(Collider other) {

        if(other.transform.GetComponent<Coin>() != null) {
            Destroy(other.gameObject);
            onCollectCoin();
        }

    }

    private void checkForSpeedArea(Collider other) {

        SpeedArea speedArea = other.GetComponent<SpeedArea>();
        if(speedArea != null) {
            if(speedArea.GetDirection == Direction.Left) {
                onSpeedAreaLeft = true;
            }
            else if(speedArea.GetDirection == Direction.Right) {
                onSpeedAreaRight = true;
            }
        }

        if(other.tag == "LongJumpBlock") {
            onLongJumpBlock = true;
        }

    }

    private void checkForEnemy(Collision other) {

        Enemy enemy = other.gameObject.GetComponent<Enemy>();
        if(enemy != null)
            if(!hasInvincibility && !enemy.Dead)
                if(!hasPowerUp)
                    Kill();
                else
                    takeAHit();
            else if(hasInvincibility)
                speed = 0;

    }

    private void takeAHit() {

        hasPowerUp = false;
        normalModel.SetActive(true);
        powerUpModel.SetActive(false);

        speed = -horizontalBounceAfterHit;

        Rigidbody body = GetComponent<Rigidbody>();
        body.velocity = new Vector3(
            body.velocity.x,
            verticalBounceAfterHit,
            body.velocity.z
        );

        ApplyInvincibility();

    }

    private void checkForPowerUp(Collider other) {

        PickUp pickUp = other.GetComponent<PickUp>();
        if(pickUp != null)
            if(pickUp.gameObject.tag == "PowerUp") {
                pickUp.Collect();
                ApplyPowerUp();
            }
            else if(pickUp.gameObject.tag == "ExtraLives") {
                pickUp.Collect();
                ApplyExtraLives();
            }

    }

    private void checkForFinishLine(Collider other) {

        if(other.tag == "FinishLine") {
            hasInvincibility = true;
            finished = true;
        }

    }

    private void checkForExitSpeedArea(Collider other) {

        SpeedArea speedArea = other.GetComponent<SpeedArea>();
        if(speedArea != null) {
            if(speedArea.GetDirection == Direction.Left) {
                onSpeedAreaLeft = false;
            }
            else if(speedArea.GetDirection == Direction.Right) {
                onSpeedAreaRight = false;
            }
        }

        if(other.tag == "LongJumpBlock") {
            onLongJumpBlock = false;
        }

    }

    private void Kill() {

        dead = true;
        GetComponent<Collider>().enabled = false;
        GetComponent<Rigidbody>().velocity = Vector3.zero;
        GetComponent<Rigidbody>().AddForce(new Vector3(0, 500f, -800f));

        playerData["lives"]--;

    }

    private void ApplyPowerUp() {

        hasPowerUp = true;
        normalModel.SetActive(false);
        powerUpModel.SetActive(true);

    }

    public void ApplyExtraLives() {

        playerData["lives"] = maxLives;

    }

    private void ApplyInvincibility() {

        hasInvincibility = true;
        StartCoroutine(InvincibilityRoutine());

    }

    private IEnumerator InvincibilityRoutine() {

        float initialWaitingTime = invincibilityDuration * 0.75f;
        int initialBlinks = 20;

        for(int i = 0; i < initialBlinks; i++) {
            model.SetActive(!model.activeSelf);
            yield return new WaitForSeconds(initialWaitingTime / initialBlinks);
        }

        float finalWaitingTime = invincibilityDuration * 0.25f;
        int finalBlinks = 30;

        for(int i = 0; i < finalBlinks; i++) {
            model.SetActive(!model.activeSelf);
            yield return new WaitForSeconds(finalWaitingTime / finalBlinks);
        }

        model.SetActive(true);

        hasInvincibility = false;

    }

}
