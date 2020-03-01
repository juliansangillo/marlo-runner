using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleEnemy : Enemy {

    public float speed = 3f;
    public float movementAmplitude = 4f;

    private Vector3 initialPosition;
    private bool movingLeft = true;

    // Start is called before the first frame update
    void Start() {

        initialPosition = transform.position;

    }

    // Update is called once per frame
    void Update() {
        
        transform.position = new Vector3(
            transform.position.x + speed * Time.deltaTime * (movingLeft ? -1 : 1),
            transform.position.y,
            transform.position.z
        );

        if(movingLeft && transform.position.x < initialPosition.x - movementAmplitude / 2) {
            movingLeft = false;
        }
        else if(!movingLeft && transform.position.x > initialPosition.x + movementAmplitude / 2) {
            movingLeft = true;
        }

    }

}
