using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

//usage: put this on a canvas that holds UI elements
//intent: display UI elements (health, score, # of lives, # of bombs, laser status)

public class UIManager : MonoBehaviour {
    public GameObject levelManager;
    public GameObject player;
    public GameObject playerToDelete; //this reference will be deleted once the player model final is in

    public TextMeshProUGUI scoreText; 
    public TextMeshProUGUI bombCountText;
    public TextMeshProUGUI lifeCountText;

    private int scoreLength = 3;
    private int bombCount;
    private int lifeCount;

    void Start() {
        bombCountText.text = player.GetComponent<PlayerShoot>().GetBombCount().ToString();
        lifeCountText.text = playerToDelete.GetComponent<HealthManager>().GetLives().ToString();
    }

    // Update is called once per frame
    void Update() {
        UpdateScore();
        UpdateBombCount();
        UpdateLifeCount();
    }

    //display the score in a format with leading 0s (ex. 001 instead of 1. 010 instead of 10, etc...)
    private void UpdateScore() {
        string currentScore = levelManager.gameObject.GetComponent<ScoreManager>().GetScore().ToString();
        int leadingZeroesNum = scoreLength - currentScore.Length;
        
        string leadingZeroes = "";

        for (int i = 0; i < leadingZeroesNum; i++) {
            leadingZeroes += "0";
        }

        scoreText.text = leadingZeroes + currentScore;
    }

    private void UpdateBombCount() {
        bombCount = player.GetComponent<PlayerShoot>().GetBombCount();

        bombCountText.text = "x   " + bombCount.ToString();
    }

    private void UpdateLifeCount() {
        lifeCount = playerToDelete.GetComponent<HealthManager>().GetLives();

        lifeCountText.text = "x   " + lifeCount.ToString();
    }
}
