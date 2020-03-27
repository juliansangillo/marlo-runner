public class StateChangeSignal {

    private string objectId;
    private string key;
    private object value;

    public StateChangeSignal(string objectId, string key, object value) {

        this.objectId = objectId;
        this.key = key;
        this.value = value;

    }

    public string ObjectId {
        get {
            return objectId;
        }
    }

    public string Key {
        get {
            return key;
        }
    }

    public object Value {
        get {
            return value;
        }
    }
    
}