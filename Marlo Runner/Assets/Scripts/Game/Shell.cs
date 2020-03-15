using UnityEngine;

public class Shell : Enemy {

    [SerializeField] private float rotatingSpeed = 180f;
    [SerializeField] private float movementSpeed = 10f;
    [SerializeField] private float radiusTolerance = 0.4f;

    private bool movingRight = true;
    private int count = 5;

    private void Update() {
        
        transform.RotateAround(transform.position, Vector3.up, rotatingSpeed * Time.deltaTime);

        transform.position = new Vector3(
            transform.position.x + movementSpeed * (movingRight ? 1 : -1) * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );

        if(count <= 0)
            GetComponent<BoxCollider>().enabled = true;
        else
            count--;

    }

    private void OnCollisionEnter(Collision other) {

        if(other.gameObject.GetComponent<Enemy>() != null) {
            Destroy(other.gameObject);
        }
        else if(other.gameObject.GetComponent<Player>() != null ||
                other.gameObject.GetComponent<Destroyer>() != null ||
                other.gameObject.tag == "JumpingArea") {
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

}
