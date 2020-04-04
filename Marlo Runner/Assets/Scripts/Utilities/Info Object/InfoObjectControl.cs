using Zenject;
using UnityEngine;

/** \file InfoObjectControl.cs
*/

/**
* A monobehaviour that is the info object control script. Its responsibility is to anchor to and maintain an info object.
* When loaded, it will search the scene for its info object. If found, it will anchor that object to itself so others can
* reference its data. If not found, it will request Zenject to instantiate a default one for it to anchor.
*
* @author Julian Sangillo
* @version 1.0
* @see InfoObjectInstaller
* @see InitializePersistentData
* @see InitializeSignal
*/
public class InfoObjectControl : MonoBehaviour {

    [SerializeField] private GameObject infoObjectPrefab = null;        ///< A reference to the prefab of an info object
    [SerializeField] private string objectTag = "";                     ///< The tag to set on a new info object used to identify that object. Please verify that such a tag exists in Unity's tag list before applying. This must be unique!

    private InfoObject infoObj;
    private CreateInfoObject createInfoObject;
    private SignalBus signalBus;

    /**
    * InfoObj property (read-only). Gets the infoObj object.
    *
    * @return The current info object anchored to this control script
    */
    public InfoObject InfoObj {
        get {
            return infoObj;
        }
    }

    [Inject]
    private void Construct(CreateInfoObject createInfoObject, SignalBus signalBus) {

        this.createInfoObject = createInfoObject;
        this.signalBus = signalBus;

    }

    private void Awake() {
        
        GameObject obj = GameObject.FindWithTag(objectTag);

        if(obj != null)
            this.infoObj = obj.GetComponent<InfoObject>();
        else
            this.infoObj = createInfoObject(infoObjectPrefab, objectTag);

    }

    private void Start() {

        signalBus.Subscribe<InitializeSignal>(this.infoObj.FireAll);
        
    }

}

/**
* A delegate function to call when requesting Zenject to provide us with a new info object.
*
* @param infoObjectPrefab The reference to the info object prefab Zenject should use to instantiate the info object
* @param objectTag The tag to set on the new info object used to identify that object.
*/
public delegate InfoObject CreateInfoObject(GameObject infoObjectPrefab, string objectTag);
