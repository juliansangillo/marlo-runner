using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using Zenject;
using UnityEngine.SceneManagement;

public class LevelManager : IInitializable {

    readonly private IDictionary<string, int> levels;

    private string[] displayName;
    private int activeLevel;

    public string LevelName {
        get {

            return displayName[activeLevel];
        }
    }

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

    //!throws ArgumentNullException, ArgumentException
    public void LoadLevel(string name) {

        int newLevel;

        if(name == null)
            throw new ArgumentNullException("Name of level cannot be null!");
        else if(!levels.TryGetValue(name, out newLevel))
            throw new ArgumentException(String.Format("The level \"{0}\" cannot be loaded because it doesn't exist!", name));

        SceneManager.LoadScene(newLevel);
        activeLevel = newLevel;

    }

    public void LoadNextLevel() {

        activeLevel++;

        if(activeLevel < SceneManager.sceneCountInBuildSettings)
            SceneManager.LoadScene(activeLevel);
        else {
            SceneManager.LoadScene(0);
            activeLevel = 0;
        }

    }

    public void ReloadLevel() {

        SceneManager.LoadScene(activeLevel);

    }

    [Inject]
    private LevelManager(IDictionary<string, int> levels) {

        this.levels = levels;

    }
    
}
