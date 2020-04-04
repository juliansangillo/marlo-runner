using System.Collections.Generic;
using Zenject;
using UnityEngine;

/** \file InfoObjectInstaller.cs
*/

/**
* A Zenject monoinstaller that installs bindings for the InfoObject API dependencies. To use, it is recommended to attach 
* this installer to the scene context.
* 
* @author Julian Sangillo
* @version 1.0
* @see IInfo
* @see Info
* @see InfoObject
* @see InfoObjectControl
*/
public class InfoObjectInstaller : MonoInstaller {
    
    /**
    * A callback from Zenject that binds InfoObject dependencies to the DI Container for future dependency injection. This is 
    * called by Zenject during binding and should NOT be called directly!
    */
    public override void InstallBindings() {

        Container.Bind<CreateInfoObject>().FromMethod(CreateCallbackForInfoObject).AsSingle();
        Container.Bind<IInfo>().To<Info>().FromMethod(CreateInfo).AsTransient();

    }

    private InfoObject OnCreateInfoObject(GameObject infoObjectPrefab, string objectTag) {

        GameObject obj = Container.InstantiatePrefab(infoObjectPrefab);
        
        obj.tag = objectTag;
        obj.GetComponent<InfoObject>().InitInfoID();
        
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