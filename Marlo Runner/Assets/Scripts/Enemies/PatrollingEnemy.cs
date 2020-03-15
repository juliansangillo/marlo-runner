using UnityEngine;

public class PatrollingEnemy : Enemy {

    [SerializeField] private float speed = 0;
    [SerializeField] private float movementAmplitude = 0;

    private Vector3 initialPosition;
    private bool movingLeft = true;

    protected virtual void Start() {

        initialPosition = transform.position;

    }

    protected virtual void Update() {
        
        move();
        holdOrChangeDirection();

    }

    private void move() {

        transform.position = new Vector3(
            transform.position.x + speed * Time.deltaTime * (movingLeft ? -1 : 1),
            transform.position.y,
            transform.position.z
        );

    }

    private void holdOrChangeDirection() {

        if(movingLeft && transform.position.x < initialPosition.x - movementAmplitude / 2)
            movingLeft = false;
        else if(!movingLeft && transform.position.x > initialPosition.x + movementAmplitude / 2)
            movingLeft = true;

    }

}
