﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour {

    public Player player;
    public Text scoreText;
    public Text endLevelText;

    private int score;
    private float restartTimer = 3f;
    private float finishTimer = 5f;
    private bool finished = false;

    // Start is called before the first frame update
    void Start() {

        player.onCollectCoin = () => {
            score++;
            scoreText.text = "Score: " + score;
        };

        endLevelText.enabled = false;

    }

    // Update is called once per frame
    void Update() {
        
        if(player.Dead) {
            restartTimer -= Time.deltaTime;
            if(restartTimer <= 0f) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

        if(player.Finished) {
            if(!finished) {
                finished = true;
                OnFinish();
            }

            finishTimer -= Time.deltaTime;
            if(finishTimer <= 0f) {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }
        }

    }

    void OnFinish() {

        endLevelText.enabled = true;
        endLevelText.text = "You beat " + SceneManager.GetActiveScene().name + "!";
        endLevelText.text += "\nYour score: " + score;

    }

}
