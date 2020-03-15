using UnityEngine;

public class Destroyer : MonoBehaviour {
    
    [SerializeField] private GameObject target = null;
    
    private void OnTriggerEnter(Collider other) {

        if(other.transform.GetComponent<Player>() != null) {
            target.SendMessage("OnKill");
            Destroy(target.gameObject);
        }

    }

}
