using System.Collections;
using UnityEngine;

public class Coin : MonoBehaviour {

    [SerializeField] private GameObject model = null;
    [SerializeField] private float rotatingSpeed = 0;

    public void Vanish() {

        StartCoroutine(VanishRoutine());

    }
    
    private void Update() {

        model.transform.RotateAround(model.transform.position, Vector3.up, rotatingSpeed * Time.deltaTime);

    }

    private IEnumerator VanishRoutine() {

        yield return new WaitForSeconds(0.5f);
        
        Destroy(this.gameObject);

    }

}
