using System.Collections.Generic;
using Zenject;

/** \file LevelManagerInstaller.cs
*/

/**
* A Zenject monoinstaller that installs bindings for the LevelManager API dependencies. To use, it is recommended to attach 
* this installer to the project context.
* 
* @author Julian Sangillo
* @version 1.0
* @see LevelManager
*/
public class LevelManagerInstaller : MonoInstaller {
    
    /**
    * A callback from Zenject that binds LevelManager and its dependencies to the DI Container for future dependency injection. This is 
    * called by Zenject during binding and should NOT be called directly!
    */
    public override void InstallBindings() {
        
        Container.BindInterfacesTo<LevelManager>().AsSingle().NonLazy();
        Container.Bind<IDictionary<string, int>>().To<Dictionary<string, int>>()
                    .AsTransient()
                    .WhenInjectedInto<LevelManager>()
                    .NonLazy();

    }

}