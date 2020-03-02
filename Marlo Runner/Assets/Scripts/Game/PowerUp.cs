using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerUp : MonoBehaviour {
    
    public GameObject model;
    public float rotatingSpeed = 200f;
    public GameObject effectPrefab;

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {

        model.transform.RotateAround(model.transform.position, Vector3.up, rotatingSpeed * Time.deltaTime);
        model.transform.RotateAround(model.transform.position, Vector3.right, rotatingSpeed * Time.deltaTime);
        model.transform.RotateAround(model.transform.position, Vector3.forward, rotatingSpeed * Time.deltaTime);
        
    }

    public void Collect() {

        GameObject effectObj = Instantiate(effectPrefab);
        effectObj.transform.SetParent(transform.parent);
        effectObj.transform.position = transform.position;

        Destroy(gameObject);

    }

}
