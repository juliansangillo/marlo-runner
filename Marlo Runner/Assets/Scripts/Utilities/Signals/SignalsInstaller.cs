using Zenject;

/** \file SignalsInstaller.cs
*/

/**
* A Zenject monoinstaller that installs bindings for the Zenject signal bus and declares the InitializeSignal and 
* StateChangeSignal. To use, it is highly-recommended to attach this installer to the project context so that the 
* signal bus and its subscriptions are preserved between scenes. All installers whose purpose is to declare signals
* should also be attached to the project context so that they are declared only once during gameplay.
* 
* @author Julian Sangillo
* @version 1.0
* @see InitializeSignal
* @see StateChangeSignal
*/
public class SignalsInstaller : MonoInstaller {

    /**
    * A callback from Zenject that installs the signal bus and its dependencies to the DI Container for future dependency injection.
    * It also declares the Utility API signals such as InitializeSignal and StateChangeSignal. This is called by Zenject during 
    * binding and should NOT be called directly!
    */
    public override void InstallBindings() {

        SignalBusInstaller.Install(Container);

        Container.DeclareSignal<InitializeSignal>();
        Container.DeclareSignal<StateChangeSignal>().OptionalSubscriber();

    }

}