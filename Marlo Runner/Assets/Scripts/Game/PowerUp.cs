using UnityEngine;

public class PowerUp : MonoBehaviour {
    
    [SerializeField] private GameObject model = null;
    [SerializeField] private float rotatingSpeed = 0;
    [SerializeField] private GameObject effectPrefab = null;

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
