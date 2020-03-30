using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Zenject;
using UnityEngine.SceneManagement;

/** \file LevelManager.cs
*/

/**
* Default implementation of ILevelManager.
*
* @author Julian Sangillo
* @version 1.0
*/
public class LevelManager : ILevelManager {

    readonly private IDictionary<string, int> levels;

    private string[] displayName;
    private int activeLevel;

    /**
    * Default implementation of ILevelManager.LevelName
    *
    * @return The display name of the active level
    */
    public string LevelName {
        get {
            return displayName[activeLevel];
        }
    }

    /**
    * Callback from Zenject.IInitializable that is invoked during Unity's Start phase. When called, it pulls the list of
    * scenes from Unity Build Settings and adds them as levels to track, it calculates and maps the display names to the 
    * levels, and it sets the active level currently.
    *
    * This function is called automatically by Zenject. Don't call this directly!
    */
    public void Initialize() {

        int level;
        int count = SceneManager.sceneCountInBuildSettings;
        string name;

        displayName = new string[count];

        for(int i = 0; i < count; i++) {
            string capsOrNonLetterRegex = @"(?<!\s)(([A-Z]+)|([^A-Za-z\s]+))";
            string underscoreRegex = @"_+";

            level = i;
            name = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

            levels.Add(name, level);

            name = Regex.Replace(name, underscoreRegex, " ");
            name = Regex.Replace(name, capsOrNonLetterRegex, m => (" " + m.Value));
            name = name.Trim();

            displayName[i] = name;
        }

        activeLevel = SceneManager.GetActiveScene().buildIndex;

    }

    /**
    * Default implementation of ILevelManager.LoadLevel(string name)
    * 
    * @param name The name of the level to load. This is the actual name of the scene as shown in Build Settings, not the display
    * name!
    * 
    * @throws ArgumentNullException Raised when the name parameter is null
    * @throws ArgumentException Raised when a level by the name provided does not exist. In the event this happens, please verify
    * the scenes added to Build Settings
    */
    public void LoadLevel(string name) {

        int newLevel;

        if(name == null)
            throw new ArgumentNullException("Name of level cannot be null!");
        else if(!levels.TryGetValue(name, out newLevel))
            throw new ArgumentException(String.Format("The level \"{0}\" cannot be loaded because it doesn't exist!", name));

        SceneManager.LoadScene(newLevel);
        activeLevel = newLevel;

    }

    /**
    * Loads next level according to the scene's build index and the order the scenes are listed in Build Settings. This allows
    * you to let LevelManager determine the next level and take you there automatically, in the case that levels are progressed
    * sequentially in your game, without having to keep track of all level names. If there is no next level, this will take you
    * back to the first scene listed in Build Settings.
    *
    * Default implementation of ILevelManager.LoadNextLevel()
    */
    public void LoadNextLevel() {

        activeLevel++;

        if(activeLevel < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(activeLevel);
        else {
            SceneManager.LoadScene(0);
            activeLevel = 0;
        }

    }

    /**
    * Default implementation of ILevelManager.ReloadLevel()
    */
    public void ReloadLevel() {

        SceneManager.LoadScene(activeLevel);

    }

    [Inject]
    private LevelManager(IDictionary<string, int> levels) {

        this.levels = levels;

    }
    
}
