using Zenject;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    [SerializeField] private Player player = null;
    [SerializeField] private LivesCounter livesCounter = null;
    [SerializeField] private Text scoreText = null;
    [SerializeField] private Text levelText = null;
    [SerializeField] private Text endLevelText = null;
    [SerializeField] private GameObject endLevelButtons = null;
    [SerializeField] private float restartTimer = 0;
    [SerializeField] private float finishTimer = 0;

    private LevelManager manager;
    private SignalBus signalBus;

    private int score = 0;
    private int playerLives = 0;
    private bool finished = false;

    public void OnContinue() {

        manager.LoadNextLevel();

    }

    public void OnQuit() {

        manager.LoadLevel("Menu");

    }

    public void UpdateState(StateChangeSignal stateChangeInfo) {

        if(stateChangeInfo.ObjectId == "PlayerInfo" && stateChangeInfo.Key == "lives") {
            playerLives = (int)stateChangeInfo.Value;
            livesCounter.UpdateLifeUI(playerLives);
        }

    }

    [Inject]
    private void Construct(LevelManager manager, SignalBus signalBus) {

        this.manager = manager;
        this.signalBus = signalBus;

    }

    private void Start() {

        signalBus.Subscribe<StateChangeSignal>(UpdateState);

        player.OnCollectCoin = () => {
            score++;
            scoreText.text = "Score: " + score;
        };

        levelText.text = manager.LevelName;

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
        if(restartTimer <= 0f)
            if(playerLives > 0)
                manager.ReloadLevel();
            else
                manager.LoadLevel("Menu");

    }

    private void OnFinish() {

        if(!finished) {
            finished = true;
            displayFinishText();
        }

        finishTimer -= Time.deltaTime;
        if(finishTimer <= 0f)
            endLevelButtons.SetActive(true);

    }

    private void displayFinishText() {

        endLevelText.enabled = true;
        endLevelText.text = "You beat " + manager.LevelName + "!";
        endLevelText.text += "\nYour score: " + score;

    }

}
