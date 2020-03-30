using Zenject;

/** \file ILevelManager.cs
*/

/**
* Interface for singletons that keeps track of the active scene and provides a more verbose API for navigating between scenes rather
* than Unity's built in SceneManagement API. It will use the list of scenes added in Unity's Build Settings as the levels to 
* track. If you want a scene to be treated as a level that can be accessed by the Level Manager, add it to the build settings!
* The filenames of the scenes should be unique, even if they are located in separate folders! To use, inject an ILevelManager 
* object wherever needed using Zenject's "Inject" attribute.<br>
*
* A Level Manager not only keeps track of the levels, it also initializes a list of display names from the actual names of
* of the levels. This allows you to reuse the level's name for UI display purposes. For example, "UltimateLevel8-1_005" will be
* mapped to the display name "Ultimate Level 8-1 005". Below are the display name rules. <br>
*
* <ul>
*   <li>Any set of letters starting with a capital is delimited with a space, except for the first one</li>
*   <li>Any set of numbers and/or special characters before the next capital or underscore is treated as its own word and delimited with a space</li>
*   <li>All underscores are replaced with spaces</li>
* </ul>
*
* @author Julian Sangillo
* @version 1.0
*/
public interface ILevelManager : IInitializable {

    /**
    * LevelName property (read-only). Gets level name.
    *
    * @return The display name of the active level
    */
    string LevelName { get; }

    /**
    * Loads the level (or scene) selected by name and sets that as the active level, while unloading the current active level.
    * This allows you to jump to whichever level you need at any point during gameplay.
    * 
    * @param name The name of the level to load. This is the actual name of the scene as shown in Build Settings, not the display
    * name!
    * 
    * @throws ArgumentNullException Raised when the name parameter is null
    * @throws ArgumentException Raised when a level by the name provided does not exist. In the event this happens, please verify
    * the scenes added to Build Settings
    */
    void LoadLevel(string name);

    /**
    * Loads the next level.
    */
    void LoadNextLevel();

    /**
    * Reloads the current active level.
    */
    void ReloadLevel();
    
}
