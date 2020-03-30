using Zenject;
using UnityEngine;

/** \file InitializePersistentData.cs
*/

/**
* A monobehaviour that will fire an InitializeSignal during Unity's start phase once attached to a game object. You can use
* this signal to initialize data, including firing other signals, across your game at the start of each scene.
*
* @author Julian Sangillo
* @version 1.0
*/
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
