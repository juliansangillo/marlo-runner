using Zenject;
using UnityEngine;

public class InitializePersistentData : MonoBehaviour {

    private SignalBus signalBus;

    [Inject]
    private void Construct(SignalBus signalBus) {

        this.signalBus = signalBus;

    }

    private void Start() {
        
        signalBus.Fire(new InitializeSignal());

    }

}
