using UnityEngine;

public class PatrollingEnemy : Enemy {

    [SerializeField] private float speed = 0;
    [SerializeField] private float movementAmplitude = 0;
    [SerializeField] private bool movingLeft = true;
    [SerializeField] private GameObject model = null;
    [SerializeField] private float rotationSpeed = 0;
    [SerializeField] private Animator animator = null;

    private Vector3 initialPosition;
    private Vector3 currentVelocity;

    protected virtual void Start() {

        initialPosition = transform.position;
        animator.SetFloat("speed", speed);

    }

    protected virtual void Update() {
        
        move();
        holdOrChangeDirection();

    }

    protected void FixedUpdate() {
        
        currentVelocity = gameObject.GetComponent<Rigidbody>().velocity;

    }

    protected void OnCollisionEnter(Collision other) {

        if(other.collider.tag == "Player")
            gameObject.GetComponent<Rigidbody>().velocity = currentVelocity;
            
        
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

        if(movingLeft)
            model.transform.rotation = Quaternion.Lerp(model.transform.rotation, Quaternion.Euler(new Vector3(0, 90, 0)), Time.deltaTime * rotationSpeed);
        else
            model.transform.rotation = Quaternion.Lerp(model.transform.rotation, Quaternion.Euler(new Vector3(0, -90, 0)), Time.deltaTime * rotationSpeed);

    }

}
