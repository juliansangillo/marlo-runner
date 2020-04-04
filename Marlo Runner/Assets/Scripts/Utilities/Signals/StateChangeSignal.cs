/** \file StateChangeSignal.cs
*/

/**
* A Zenject signal that can be fired from the signal bus every time the state of an object with key-value pairs change.
*
* @author Julian Sangillo
* @version 1.0
*/
public class StateChangeSignal {

    private string objectId;
    private string key;
    private object value;

    /**
    * Main constructor.
    * 
    * @param objectId Unique identifier of the object that changed state
    * @param key The key in the object whose value was changed
    * @param value The new value
    */
    public StateChangeSignal(string objectId, string key, object value) {

        this.objectId = objectId;
        this.key = key;
        this.value = value;

    }

    /**
    * ObjectId property (read-only). Gets the objectId.
    *
    * @return The object's identifier
    */
    public string ObjectId {
        get {
            return objectId;
        }
    }

    /**
    * Key property (read-only). Gets the key.
    *
    * @return The key whose value was changed
    */
    public string Key {
        get {
            return key;
        }
    }

    /**
    * Value property (read-only). Gets the value.
    *
    * @return The new value
    */
    public object Value {
        get {
            return value;
        }
    }
    
}