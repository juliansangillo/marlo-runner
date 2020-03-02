using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

    public GameObject referenceObj;
    public float elementSize;
    public float elementOffset;

    private List<GameObject> backgroundElements;
    
    // Start is called before the first frame update
    void Start() {
        
        backgroundElements = new List<GameObject>();

        for(int i = 0; i < transform.childCount; i++) {
            backgroundElements.Add(transform.GetChild(i).gameObject);
        }

    }

    // Update is called once per frame
    void Update() {
        foreach(GameObject element in backgroundElements) {
            if(referenceObj.transform.position.x - element.transform.position.x > elementOffset) {
                element.transform.position = new Vector3(
                    element.transform.position.x + backgroundElements.Count * elementSize,
                    element.transform.position.y,
                    element.transform.position.z
                );
            }
        }
    }

}
