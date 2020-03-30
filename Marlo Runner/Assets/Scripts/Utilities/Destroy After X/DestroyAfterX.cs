using UnityEngine;

/** \file DestroyAfterX.cs
*/

/**
* A monobehaviour that destroys the game object it is attached to after some period of time. The user
* can decide whether it destroys after seconds, minutes, or hours.
*
* @author   Julian Sangillo
* @version  1.0
*/
public class DestroyAfterX : MonoBehaviour {
    
    [SerializeField] private TimeInterval interval = TimeInterval.Seconds;      ///< The interval that the timer is measured in.
    [SerializeField] private float timer = 0;                                   ///< Length of time until game object is destroyed.
    

    private void Start() {

        timer *= (int)interval;

    }

    private void Update() {

        timer -= Time.deltaTime;
        if(timer <= 0f)
            Destroy(gameObject);
        
    }

}
