using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndScreenManager : MonoBehaviour {
    public TextMeshProUGUI enemyDownCountText;
    public TextMeshProUGUI accumulatedTotalCountText;
    public TextMeshProUGUI finalLifeCountText;
    
    public GameObject levelManager;

    private string enemyDownCount;
    private string finalLifeCount;

    void Start() {
        enemyDownCount = levelManager.gameObject.GetComponent<ScoreManager>().GetScore().ToString();

        enemyDownCountText.text = enemyDownCount;
        accumulatedTotalCountText.text = "Accumulated total: " + enemyDownCount;
    }
}
