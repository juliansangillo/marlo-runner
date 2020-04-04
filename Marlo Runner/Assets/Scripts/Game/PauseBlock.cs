using UnityEngine;

public class PauseBlock : MonoBehaviour {

    private void OnTriggerEnter(Collider other) {

        Player player = other.GetComponent<Player>();

        if(player != null) {
            player.Pause();
            GetComponent<BoxCollider>().enabled = false;
        }

    }

}
