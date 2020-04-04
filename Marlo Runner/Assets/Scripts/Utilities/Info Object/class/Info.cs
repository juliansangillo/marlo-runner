using System;
using System.Collections.Generic;

/** \file Info.cs
*/

/**
* Stores info obout other objects in the game using key-value pairs. Keys must be strings while
* values could be of any type.
*
* Default implementation of IInfo
* 
* @author Julian Sangillo
* @version 1.0
*
* @see InfoObject
*/
public class Info : IInfo {

    private IDictionary<string, object> data;
    private StateChange stateChanged;

    private string id;

    /**
    * Main Constructor
    *
    * @param data A hash table for storing the info added by other objects
    */
    public Info(IDictionary<string, object> data) {

        this.data = data;

    }

    /**
    * Default implementation of IInfo.Data
    *
    * @return The current hash table where all info is stored
    */
    public IDictionary<string, object> Data {
        get {
            return data;
        }
    }

    /**
    * Default implementation of IInfo.StateChanged
    *
    * @param stateChanged The callback function to be called when a value stored in this info object has been changed
    */
    public StateChange StateChanged {
        set {
            stateChanged = value;
        }
    }

    /**
    * Default implementation of IInfo.Id
    *
    * @param id (Set only) A string that can be used as a unique identifier for this collection of info
    *
    * @return The current info id
    */
    public string Id {
        get {
            return id;
        }
        set {
            id = value;
        }
    }

    /**
    * A custom indexer for the data in this info object. When getting values with the indexer, it will automatically 
    * cast the result to the appropriate type.
    *
    * Default implementation of <a href="d3/dd1/interface_i_info.html#a7ade3a9fcbb1f791627e2ab87495d986">IInfo.this[string key]</a>
    *
    * @param key The key string that uniquely identifies a stored value
    * @param value (Set only) The value to assign to the given key. May be of any type
    *
    * @return The value identified by its key
    *
    * @throws ArgumentNullException Raised when the key parameter is null
    * @throws ArgumentException Raised when the key parameter is an empty string
    * @throws KeyNotFoundException (Get only) Raised when the key parameter doesn't exist in this info object
    */
    public dynamic this[string key] {
        get {
            object val = Get(key);

            if(val is int)
                return (int)val;
            else if(val is float)
                return (float)val;
            else if(val is char)
                return (char)val;
            else if(val is bool)
                return (bool)val;
            else if(val is string)
                return (string)val;
            else
                return val;
        }
        set {
            Set(key, value);
        }
    }

    /**
    * Default implementation of IInfo.Get(string key)
    *
    * @param key The key string that uniquely identifies a stored value
    * 
    * @return The value of the key
    *
    * @throws ArgumentNullException Raised when the key parameter is null
    * @throws ArgumentException Raised when the key parameter is an empty string
    * @throws KeyNotFoundException Raised when the key parameter doesn't exist in this info object
    */
    public object Get(string key) {

        if(string.IsNullOrEmpty(key))
            if(key == null)
                throw new ArgumentNullException("Cannot find value when key is 'null'");
            else
                throw new ArgumentException("Cannot find value when key is an 'empty_string'");

        object value;

        if(!data.TryGetValue(key, out value))
            throw new KeyNotFoundException("Unable to return value of an unknown key!");

        return value;
    }

    /**
    * Default implementation of IInfo.Set(string key, object value)
    * 
    * @param key The key string that uniquely identifies a stored value
    * @param value The value to assign to the given key. May be of any type
    *
    * @throws ArgumentNullException Raised when the key parameter is null
    * @throws ArgumentException Raised when the key parameter is an empty string
    */
    public void Set(string key, object value) {
        
        if(string.IsNullOrEmpty(key))
            if(key == null)
                throw new ArgumentNullException("Cannot set value of a 'null' key");
            else
                throw new ArgumentException("Cannot set value of an 'empty_string' key");

        if(data.ContainsKey(key))
            data[key] = value;
        else
            data.Add(key, value);

        stateChanged(this.Id, key, value);

    }

    /**
    * Default implementation of IInfo.Exists(string key)
    *
    * @param key The key string that uniquely identifies a stored value
    *
    * @return True if info contains the key or false otherwise
    *
    * @throws ArgumentNullException Raised when the key parameter is null
    * @throws ArgumentException Raised when the key parameter is an empty string
    */
    public bool Exists(string key) {

        if(string.IsNullOrEmpty(key))
            if(key == null)
                throw new ArgumentNullException("Cannot find a 'null' key");
            else
                throw new ArgumentException("Cannot find an 'empty_string' key");
                
        return data.ContainsKey(key);
    }

}
