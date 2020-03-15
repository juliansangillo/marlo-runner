using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] private Player player = null;
    [SerializeField] private Text scoreText = null;
    [SerializeField] private Text levelText = null;
    [SerializeField] private Text endLevelText = null;
    [SerializeField] private GameObject endLevelButtons = null;
    [SerializeField] private float restartTimer = 0;
    [SerializeField] private float finishTimer = 0;

    private int score;
    private bool finished = false;

    public void OnContinue() {

        LevelManager.Instance.LoadNextLevel();

    }

    public void OnQuit() {

        LevelManager.Instance.LoadLevel("Menu");

    }

    private void Start() {

        player.OnCollectCoin = () => {
            score++;
            scoreText.text = "Score: " + score;
        };

        levelText.text = LevelManager.Instance.LevelName;

        endLevelText.enabled = false;
        endLevelButtons.SetActive(false);

    }

    private void Update() {
        
        if(player.Dead)
            OnDead();

        if(player.Finished)
            OnFinish();

    }

    private void OnDead() {

        restartTimer -= Time.deltaTime;
        if(restartTimer <= 0f) {
            LevelManager.Instance.ReloadLevel();
        }

    }

    private void OnFinish() {

        if(!finished) {
            finished = true;
            displayFinishText();
        }

        finishTimer -= Time.deltaTime;
        if(finishTimer <= 0f) {
            endLevelButtons.SetActive(true);
            //LevelManager.Instance.LoadNextLevel();
        }

    }

    private void displayFinishText() {

        endLevelText.enabled = true;
        endLevelText.text = "You beat " + LevelManager.Instance.LevelName + "!";
        endLevelText.text += "\nYour score: " + score;

    }

}
