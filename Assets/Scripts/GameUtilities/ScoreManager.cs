using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//usage: put this on an empty GameObject that will never be destroyed 
//intent: keep track of the player's score, which is reflected by how many objects have been destroyed

public class ScoreManager : MonoBehaviour
{
    private int score = 0;

    public void ScoreSetter() {
        //both asteroids and enemies alike are worth 1 point
        score += 1;
    }

    public int GetScore() {
        return score; 
    }
}
