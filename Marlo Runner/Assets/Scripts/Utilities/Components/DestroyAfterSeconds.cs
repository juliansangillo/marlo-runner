using UnityEngine;

public class DestroyAfterSeconds : MonoBehaviour {

    [SerializeField] private float timer = 0;

    void Update() {

        timer -= Time.deltaTime;
        if(timer <= 0f)
            Destroy(gameObject);
        
    }

}
