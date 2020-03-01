using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Player player;
    public Text scoreText;

    private int score;

    // Start is called before the first frame update
    void Start() {

        player.onCollectCoin = () => {
            score++;
            scoreText.text = "Score: " + score;
        };

    }

    // Update is called once per frame
    void Update() {
        
    }

}
