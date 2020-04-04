using System.Collections.Generic;
using Zenject;
using UnityEngine;

/** \file InfoObject.cs
*/

/**
* A monobehaviour that acts as an info object for other game objects and is a wrapper for the IInfo objects.<br>
* The info object game design pattern uses two components: an object marked as DontDestroyOnLoad called an info object
* that can store game object data, and a control script to search the scene for a game object's corresponding info object.
* If an info object exists, it anchors this object to the targeted game object. If it doesn't exist, it instantiates one, 
* then anchors it. The targeted game object can then reference and store data in this object so that that data will persist 
* between scenes. This pattern makes it possible to persist data between scenes within these info objects as well as allow 
* the developer to test higher levels without having to run from level 1 to level 100, for example, on each play cycle.
*
* @author Julian Sangillo
* @version 1.0
* @see Info
* @see InfoObjectControl
* @see StateChangeSignal
*/
public class InfoObject : MonoBehaviour {
    
    private IInfo info;
    private SignalBus signalBus;

    /**
    * Initializes the IInfo object's unique id as the tag of the game object this mono is attached to.
    */
    public void InitInfoID() {

        this.info.Id = gameObject.tag;

    }

    /**
    * Gets the IInfo for others to modify
    * 
    * @return The current IInfo object
    */
    public IInfo GetInfo() {

        return this.info;
    }

    /**
    * Fires a StateChangeSignal of all stored keys and their values in this info object. So, if there are 5 key-value pairs,
    * the StateChangeSignal will be fired 5 times. This is used to initialize everything in the game that might need that data
    * at the start of each scene when objects and scripts are loaded for the first time.
    */
    public void FireAll() {
        
        ICollection<string> keys = this.info.Data.Keys;

        foreach(string key in keys)
            signalBus.Fire(new StateChangeSignal(this.info.Id, key, this.info[key]));

    }

    [Inject]
    private void Construct(IInfo info, SignalBus signalBus) {

        this.info = info;
        this.signalBus = signalBus;

    }

    private void Awake() {
        
        this.info.StateChanged = new StateChange(OnStateChange);

    }

    private void OnStateChange(string id, string key, object value) {

        signalBus.Fire(new StateChangeSignal(id, key, value));

    }

}

/**
* A delegate function that will fire the StateChangeSignal when a callback is recieved from the IInfo. This will
* happen when the value of one of its keys was modified.
*
* @param id The unique identifier (the gameObject tag) that fired the signal so the subscribers to the signal know which
* info object had their state changed
* @param k The key that uniquely identifies the value that changed
* @param v The new value of the key. May be of any type
*/
public delegate void StateChange(string id, string k, dynamic v);
