using System.Collections.Generic;

public interface IInfo {
    
    IDictionary<string, object> Data { get; }
    StateChange StateChanged { set; }
    string Id { get; set; }

    dynamic this[string key] { get; set; }

    object Get(string key);
    void Set(string key, object value);
    bool Exists(string key);

}
