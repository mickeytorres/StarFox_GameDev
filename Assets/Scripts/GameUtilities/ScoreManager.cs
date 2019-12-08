using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put this on an empty GameObject that will never be destroyed 
//intent: keep track of the player's score, which is reflected by how many objects have been destroyed

public class ScoreManager : MonoBehaviour {
    private int score = 0;

    //public function any object that anything that rewards points for being destroyed can call to increment score
    public void ScoreSetter() {
        //both asteroids and enemies alike are worth 1 point
        Debug.Log("incrementing the score");
        score += 1;
    }

    //public function any object that requires knowing the score can call
    public int GetScore() {
        //999 not achievable, but set a max for safety
        return Mathf.Clamp(score, 0, 999);
    }
}
