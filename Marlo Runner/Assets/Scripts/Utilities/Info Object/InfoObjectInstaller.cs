using System.Collections.Generic;
using Zenject;
using UnityEngine;

public class InfoObjectInstaller : MonoInstaller {
    
    public override void InstallBindings() {

        Container.Bind<CreateInfoObject>().FromMethod(CreateCallbackForInfoObject).AsSingle();
        Container.Bind<IInfo>().To<Info>().FromMethod(CreateInfo).AsTransient();

    }

    private InfoObject OnCreateInfoObject(GameObject infoObjectPrefab, string objectTag) {

        GameObject obj = Container.InstantiatePrefab(infoObjectPrefab);
        
        obj.tag = objectTag;
        obj.GetComponent<InfoObject>().InitInfo();
        
        return obj.GetComponent<InfoObject>();
    }

    private CreateInfoObject CreateCallbackForInfoObject() {

        return new CreateInfoObject(OnCreateInfoObject);
    }

    private Info CreateInfo() {

        IDictionary<string, object> data = new Dictionary<string, object>();

        return new Info(data);
    }

}