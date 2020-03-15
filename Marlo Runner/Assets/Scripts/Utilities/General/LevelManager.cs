using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using UnityEngine.SceneManagement;

public class LevelManager {

    private static LevelManager instance;

    private Dictionary<string, int> levels = new Dictionary<string, int>();
    private string[] displayName;

    private int activeLevel;

    public static LevelManager Instance {
        get {

            if(instance == null) {
                instance = new LevelManager();
            }
            return instance;
        }
    }

    public string LevelName {
        get {

            return displayName[activeLevel];
        }
    }

    public void LoadLevel(string name) {

        int newLevel;

        if(levels.ContainsKey(name)) {
            newLevel = levels[name];

            SceneManager.LoadScene(newLevel);
            activeLevel = newLevel;
        }

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

    private LevelManager() {

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
    
}
