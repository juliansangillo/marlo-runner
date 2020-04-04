using UnityEngine;

public class CameraFollow : MonoBehaviour {

    [SerializeField] private Player player = null;
    [SerializeField] private Vector3 offset = Vector3.zero;
    [SerializeField] private float speed = 0;

    private void FixedUpdate() {

        if(!player.Dead && !player.Finished) {
            Vector3 targetPosition = new Vector3(player.transform.position.x + offset.x, player.transform.position.y + offset.y, transform.position.z + offset.z);
            transform.position = Vector3.Lerp(transform.position, targetPosition, speed * Time.deltaTime);
        }
        
    }

}
