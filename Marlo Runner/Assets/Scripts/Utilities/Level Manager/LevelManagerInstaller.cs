using System.Collections.Generic;
using Zenject;

public class LevelManagerInstaller : MonoInstaller {
    
    public override void InstallBindings() {
        
        Container.BindInterfacesAndSelfTo<LevelManager>().AsSingle().NonLazy();
        Container.Bind<IDictionary<string, int>>().To<Dictionary<string, int>>()
                    .AsTransient()
                    .WhenInjectedInto<LevelManager>()
                    .NonLazy();

    }

}