using UnityEngine;

public enum TimeInterval { Seconds = 1, Minutes = 60, Hours = 3600 }

public class DestroyAfterX : MonoBehaviour {

    [SerializeField] private TimeInterval interval = TimeInterval.Seconds;
    [SerializeField] private float timer = 0;

    private void Start() {

        timer *= (int)interval;

    }

    void Update() {

        timer -= Time.deltaTime;
        if(timer <= 0f)
            Destroy(gameObject);
        
    }

}
