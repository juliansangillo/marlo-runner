using UnityEngine;

public class Shell : Enemy {

    [SerializeField] private float rotatingSpeed = 180f;
    [SerializeField] private float movementSpeed = 10f;

    private bool movingRight = true;
    private float count = 0.1f;

    private void Start() {
        
        GetComponent<BoxCollider>().enabled = false;

    }

    private void Update() {
        
        transform.RotateAround(transform.position, Vector3.up, rotatingSpeed * Time.deltaTime);

        transform.position = new Vector3(
            transform.position.x + movementSpeed * (movingRight ? 1 : -1) * Time.deltaTime,
            transform.position.y,
            transform.position.z
        );

        count -= Time.deltaTime;
        if(count <= 0)
            GetComponent<BoxCollider>().enabled = true;

    }

    private void OnCollisionEnter(Collision other) {

        if(other.gameObject.GetComponent<Enemy>() != null) {
            Destroy(other.gameObject);
        }
        else if(other.gameObject.GetComponent<Player>() != null ||
                other.gameObject.GetComponent<Destroyer>() != null ||
                other.transform.position.y + other.transform.localScale.y / 2 < transform.position.y) {
            return;
        }
        else {
            if(transform.position.x < other.transform.position.x && movingRight) {
                movingRight = false;
            }
            else if(transform.position.x > other.transform.position.x && !movingRight) {
                movingRight = true;
            }
        }
        
    }

}
