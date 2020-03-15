using UnityEngine;

public class PlantEnemy : Enemy {

    [SerializeField] private float visibleHeight = 0;
    [SerializeField] private float hiddenHeight = 0;
    [SerializeField] private float movementSpeed = 0;
    [SerializeField] private float waitingDuration = 0;

    private bool hiding = true;
    private float waitingTimer = 0f;
    
    private void Start() {

        waitingTimer = waitingDuration;

    }

    private void Update() {

        if(hiding)
            if(transform.localPosition.y > hiddenHeight)
                recede();
            else
                wait(false);
        else
            if(transform.localPosition.y < visibleHeight)
                rise();
            else
                wait(true);

    }

    private void rise() {

        transform.localPosition = new Vector3 (
            transform.localPosition.x,
            transform.localPosition.y + movementSpeed * Time.deltaTime,
            transform.localPosition.z
        );

    }

    private void recede() {

        transform.localPosition = new Vector3 (
            transform.localPosition.x,
            transform.localPosition.y - movementSpeed * Time.deltaTime,
            transform.localPosition.z
        );

    }

    private void wait(bool willHide) {

        waitingTimer -= Time.deltaTime;
        if(waitingTimer <= 0f) {
            waitingTimer = waitingDuration;
            hiding = willHide;
        }

    }

}
