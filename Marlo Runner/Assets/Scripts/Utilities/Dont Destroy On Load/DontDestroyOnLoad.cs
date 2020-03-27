using UnityEngine;

public class DontDestroyOnLoad : MonoBehaviour {

    [SerializeField] private bool preserveDuplicates = false;

    private void Start() {
        
        if(!preserveDuplicates) {
            GameObject[] objects = GameObject.FindGameObjectsWithTag(gameObject.tag);

            if(objects.Length > 1) {
                Destroy(gameObject);
                return;
            }
        }

        DontDestroyOnLoad(gameObject);
        
    }

}
