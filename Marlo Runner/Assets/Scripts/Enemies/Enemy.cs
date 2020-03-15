using UnityEngine;

public class Enemy : MonoBehaviour {

    protected bool dead = false;

    [SerializeField] protected GameObject player = null;
    
    public bool Dead {
        get {
            return dead;
        }
    }
    
    protected virtual void OnKill() {

        dead = true;
        GetComponent<BoxCollider>().enabled = false;
        player.GetComponent<Player>().Jump(true);

    }

}
