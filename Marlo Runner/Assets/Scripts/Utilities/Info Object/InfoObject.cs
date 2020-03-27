using System.Collections.Generic;
using Zenject;
using UnityEngine;

public class InfoObject : MonoBehaviour {
    
    private IInfo info;
    private SignalBus signalBus;

    [Inject]
    public void Construct(IInfo info, SignalBus signalBus) {

        this.info = info;
        this.signalBus = signalBus;

    }

    public void InitInfo() {

        this.info.Id = gameObject.tag;

    }

    public IInfo GetInfo() {

        return this.info;
    }

    public void FireAll() {
        
        ICollection<string> keys = this.info.Data.Keys;

        foreach(string key in keys)
            signalBus.Fire(new StateChangeSignal(this.info.Id, key, this.info[key]));

    }

    private void Awake() {
        
        this.info.StateChanged = new StateChange(OnStateChange);

    }

    private void OnStateChange(string id, string key, object value) {

        signalBus.Fire(new StateChangeSignal(id, key, value));

    }

}

public delegate void StateChange(string id, string k, dynamic v);
