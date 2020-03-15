using UnityEngine;

public class ShellEnemy : PatrollingEnemy {

    [SerializeField] private GameObject shellPrefab = null;
    
    protected override void Start() {
        base.Start();

    }

    protected override void Update() {
        base.Update();

    }

    protected override void OnKill() {
        base.OnKill();

        GameObject shellObj = Instantiate(shellPrefab);
        shellObj.transform.position = transform.position;
        shellObj.transform.parent = transform.parent;

    }

}
