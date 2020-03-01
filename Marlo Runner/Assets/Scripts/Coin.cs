using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour {

    public GameObject model;
    public float rotatingSpeed = 30f;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        model.transform.RotateAround(model.transform.position, Vector3.up, rotatingSpeed * Time.deltaTime);
    }

}
