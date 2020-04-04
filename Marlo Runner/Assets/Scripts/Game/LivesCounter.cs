using UnityEngine;
using UnityEngine.UI;

public class LivesCounter : MonoBehaviour {

    [SerializeField] private Image[] lives = null;

    private int count;

    public void UpdateLifeUI(int newLives) {

        int diff = CalculateDifference(newLives);

        if(newLives == 0 || diff == 0)
            return;
        else if(diff < 0)
            for(int i = count - 1; diff < 0; diff++) {
                lives[i].enabled = false;
                i--;
            }
        else
            for(int i = count; diff > 0; diff--) {
                if(lives[i] == null) {
                    i++;
                    continue;
                }

                lives[i].enabled = true;
                i++;
            }

    }

    private void Start() {
        
        Init();
        count = 0;

    }

    private void Init() {

        foreach(Image life in lives) {
            if(life == null)
                continue;

            life.enabled = false;
        }

    }

    private int CalculateDifference(int playerLives) {
        
        return playerLives - count;
    }

}
