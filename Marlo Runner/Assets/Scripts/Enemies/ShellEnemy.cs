using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShellEnemy : PatrollingEnemy {

    public GameObject shellPrefab;
    
    // Start is called before the first frame update
    protected override void Start() {
        base.Start();
    }

    // Update is called once per frame
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
