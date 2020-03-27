using System;
using System.Collections.Generic;

public class Info : IInfo {

    private IDictionary<string, object> data;
    private StateChange stateChanged;

    private string id;

    public Info(IDictionary<string, object> data) {

        this.data = data;

    }

    public IDictionary<string, object> Data {
        get {
            return data;
        }
    }

    public StateChange StateChanged {
        set {
            stateChanged = value;
        }
    }

    public string Id {
        get {
            return id;
        } 
        set {
            id = value;
        }
    }

    //! throws ArgumentNullException, ArgumentException, KeyNotFoundException
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

    //! throws ArgumentNullException, ArgumentException, KeyNotFoundException
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

    //! throws ArgumentNullException, ArgumentException
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

    public bool Exists(string key) {

        if(string.IsNullOrEmpty(key))
            if(key == null)
                throw new ArgumentNullException("Cannot find a 'null' key");
            else
                throw new ArgumentException("Cannot find an 'empty_string' key");
                
        return data.ContainsKey(key);
    }

}
