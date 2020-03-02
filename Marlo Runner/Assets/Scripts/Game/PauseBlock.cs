using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseBlock : MonoBehaviour {

    // Start is called before the first frame update
    void Start() {
        
    }

    // Update is called once per frame
    void Update() {
        
    }

    void OnTriggerEnter(Collider col) {

        Player player = col.GetComponent<Player>();

        if(player != null) {
            player.Pause();
            GetComponent<BoxCollider>().enabled = false;
        }

    }

}
