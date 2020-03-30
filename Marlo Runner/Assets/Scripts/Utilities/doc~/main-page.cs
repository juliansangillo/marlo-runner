/** \mainpage Utilities Main Page
* 
* \section intro Introduction
* This library consists of a set of tools and APIs that I have abstracted from my projects and made them reusable so they
* can be integrated into any project. This prevents me from having to "reinvent the wheel" with each project and with future
* projects. This toolkit will evolve as I make more projects and find more features that could be reused, and when I discover
* what solutions are efficient and which ones are not efficient.
*
* \subsection about About Me
* Name: Julian Sangillo<br>
* Linkedin: https://www.linkedin.com/in/thatdevjulian<br>
* Github: https://github.com/juliansangillo
*
* \section install Installation
* <ol>
*   <li>Retrieve the Utilities unity package from my GitHub</li>
*   <li>Open a Unity project</li>
*   <li>Go to Assets > Import Package > Custom Package</li>
*   <li>Select the unitypackage file and hit Open</li>
*   <li>The code should then be imported under Assets\Scripts\Utilities</li>
* </ol>
*
* \section plugins Plugins
* This package comes with some helpful (and free) plugins right from the Asset store. Some of the code in Utilities depend on these
* plugins.
*
* \subsection zenject-plugin Zenject
* This package comes with Zenject, a dependency injection framework that makes it easier to distribute any dependencies your code
* may need across your project. This package uses Zenject to inject the objects from this package into your code so it is easier
* to integrate.<br>
* This package also comes with Zenject Signals. Signals are Zenject's implementation of the Observer design pattern
* where you can subscribe functions to a Signal and all subscribers get notified when that Signal is fired. This allows one part
* of your code to fire a Signal that triggers an event and sends that event data in a completely different part of your code without 
* directly referencing the objects that need it. Signals allow for effective communication between components without coupling those
* components.<br>
* More details on Zenject's DI Container and Signals can be found on Zenject's documentation page.
*
* \section how-to-use How To Use
* Once the package is imported into Unity, some of the tools you may be able to use right away, but for all the tools to work properly
* you need to setup the Zenject contexts and installers.
* <ol>
*   <li>Add the project context by going to Assets > Create > Zenject > Project Context. This creates one in Resources.</li>
*   <li>Add the scene context to your open scene by going to the Hierarchy and clicking Create > Zenject > Scene Context.</li>
*   <li>If you are going to make use of the Level Manager API, go to Utilities\Level Manager and attach the LevelManagerInstaller
*   to the project context and add the component to the list of MonoInstallers.</li>
*   <li>If you are going to make use of the Info Object API, go to Utilities\Info Object and attach the InfoObjectInstaller to
*   the scene context and add the component to MonoInstallers. This should be added to the scene context for all scenes.</li>
*   <li>Make sure to attach the SignalsInstaller located in Utilities\Signals to the project context and add the component to
*   MonoInstallers. This is required for the Signals to work correctly.</li>
* </ol>
* NOTE: All Signal Installers you create for Signals in your project should be attached to the project context, not the scene context,
* for the subscribers to carry over between scenes.<br>
* After setting up the installers, you can use Zenject's "Inject" attribute to inject the required objects from Utilities into your
* code for you to use.
*
* \section included What's Included?
* Destroy After X - A component you can attach to objects you want to be destroyed after a specified interval and time period<br>
* Dont Destroy On Load - A component you can attach to objects you don't want to be destroyed after a new level loads<br>
* Info Object API - Provides data storage in the form of info objects that are tied to specific game objects and persist between scenes<br>
* Level Manager API - Better level navigation than Unity's SceneManagement<br>
* Signals - Includes a base signal installer for your project and some signals used by this package
* 
*/