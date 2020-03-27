using Zenject;
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

    [Inject]
    private void Construct(LevelManager manager) {

        this.manager = manager;

    }

    private void Start() {

        GameObject obj = GameObject.FindWithTag("PlayerInfo");

        if(obj != null) {
            IInfo playerData = obj.GetComponent<InfoObject>().GetInfo();
            playerData["lives"] = playerData["maxLives"];
        }

    }

}
