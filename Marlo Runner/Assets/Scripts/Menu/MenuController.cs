using UnityEngine;

public class MenuController : MonoBehaviour {

    [SerializeField] private GameObject helpPanel = null;

    private LevelManager manager;

    public void OnPressPlay() {

        manager.LoadNextLevel();

    }

    public void OnPressHelp() {

        helpPanel.SetActive(true);

    }

    public void OnPressQuit() {

        Application.Quit();

    }

    private void Awake() {

        manager = LevelManager.Instance;
        
    }

}
