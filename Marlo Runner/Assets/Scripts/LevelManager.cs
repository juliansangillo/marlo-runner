using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager {

    private static LevelManager instance;

    private const int LEVELS_PER_WORLD = 4;

    private int world = 1;
    private int level = 1;

    private LevelManager() {}

    public static LevelManager Instance {
        get {

            if(instance == null) {
                instance = new LevelManager();
            }
            return instance;
        }
    }

    public int World {
        get {

            return world;
        }
    }

    public int Level {
        get {

            return level;
        }
    }

    public string LevelName {
        get {

            return "Level " + world + "-" + level;
        }
    }

    public void LoadLevel(int world, int level) {

        this.world = world;
        this.level = level;

        SceneManager.LoadScene("Level" + world + "-" + level);

    }

    public void LoadNextLevel() {

        this.level++;

        if(this.level > LEVELS_PER_WORLD) {
            this.level = 1;
            this.world++;
        }

        LoadLevel(this.world, this.level);

    }

    public void ReloadLevel() {

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    
}
