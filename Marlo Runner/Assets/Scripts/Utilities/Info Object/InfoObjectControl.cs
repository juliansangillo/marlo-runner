using Zenject;
using UnityEngine;

public class InfoObjectControl : MonoBehaviour {

    [SerializeField] private GameObject infoObjectPrefab = null;
    [SerializeField] private string objectTag = "";

    private InfoObject infoObj;
    private CreateInfoObject createInfoObject;
    private SignalBus signalBus;

    public InfoObject InfoObj {
        get {
            return infoObj;
        }
    }

    [Inject]
    public CreateInfoObject CreateInfoObject {
        set {
            createInfoObject = value;
        }
    }

    [Inject]
    private void Construct(SignalBus signalBus) {

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

public delegate InfoObject CreateInfoObject(GameObject infoObjectPrefab, string objectTag);
