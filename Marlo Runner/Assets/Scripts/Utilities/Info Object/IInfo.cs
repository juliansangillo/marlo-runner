using System.Collections.Generic;

/** \file IInfo.cs
*/

/**
* Interface for objects that store info on other game objects for the purpose of persisting this data across scenes. It is
* recommended to interact with Info objects using this interface rather than the concrete implementation.
*
* @author Julian Sangillo
* @version 1.0
* @see Info
*/
public interface IInfo {
    
    /**
    * Data property (read-only). Gets the data object.
    *
    * @return The current hash table where all info is stored
    */
    IDictionary<string, object> Data { get; }

    /**
    * StateChanged property (write-only). Sets the stateChanged delegate.
    * 
    * @param stateChanged The callback function to be called when a value stored in this info object has been changed
    */
    StateChange StateChanged { set; }

    /**
    * Id property. Gets and Sets the id.
    *
    * @param id (Set only) A string that can be used as a unique identifier for this collection of info
    *
    * @return The current info id
    */
    string Id { get; set; }

    /**
    * A custom indexer for the data in this info object. When getting values with the indexer, it will automatically 
    * cast the result to the appropriate type. When setting values, if the key does not already exist, it will add
    * one.
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
    dynamic this[string key] { get; set; }

    /**
    * Gets the value of the provided key. Returned value must be manually casted to the needed type.
    *
    * @param key The key string that uniquely identifies a stored value
    * 
    * @return The value of the key
    *
    * @throws ArgumentNullException Raised when the key parameter is null
    * @throws ArgumentException Raised when the key parameter is an empty string
    * @throws KeyNotFoundException Raised when the key parameter doesn't exist in this info object
    */
    object Get(string key);

    /**
    * Sets the value of the provided key. When setting values, if the key does not already exist, it will add
    * one.
    *
    * @param key The key string that uniquely identifies a stored value
    * @param value The value to assign to the given key. May be of any type
    *
    * @throws ArgumentNullException Raised when the key parameter is null
    * @throws ArgumentException Raised when the key parameter is an empty string
    */
    void Set(string key, object value);

    /**
    * Check if the key exists in this info object
    *
    * @param key The key string that uniquely identifies a stored value
    *
    * @return True if info contains the key or false otherwise
    *
    * @throws ArgumentNullException Raised when the key parameter is null
    * @throws ArgumentException Raised when the key parameter is an empty string
    */
    bool Exists(string key);

}
