using System.Collections.Generic;
using UnityEngine;

public class ParallaxBackground : MonoBehaviour {

    [SerializeField] private GameObject referenceObj = null;
    [SerializeField] private float elementSize = 0;
    [SerializeField] private float elementOffset = 0;
    [SerializeField] private float speed = 0;

    private List<GameObject> backgroundElements;
    
    private void Start() {
        
        backgroundElements = new List<GameObject>();

        for(int i = 0; i < transform.childCount; i++)
            backgroundElements.Add(transform.GetChild(i).gameObject);

    }

    private void Update() {
        
        offsetOutOfBoundElements();
        moveElementsLeft();
        
    }

    private void offsetOutOfBoundElements() {

        foreach(GameObject element in backgroundElements)
            if(referenceObj.transform.position.x - element.transform.position.x > elementOffset)
                element.transform.position = new Vector3(
                    element.transform.position.x + backgroundElements.Count * elementSize,
                    element.transform.position.y,
                    element.transform.position.z
                );

    }

    private void moveElementsLeft() {

        foreach(GameObject element in backgroundElements)
            element.transform.position += Vector3.left * speed * Time.deltaTime;

    }

}
