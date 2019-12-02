using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private bool pause = false;
    private bool play = true;

    void Update()
    {
        //eventually will want some kind of "press [ ] to start game"
        if (Input.GetKey(KeyCode.Return)) {
            SceneManager.LoadScene(0);
        } 
    }

    //function to pause in-game
    public void Pause() {
        if (Input.GetKey(KeyCode.Escape)) {
            if (!pause) {
                Time.timeScale = 0;
                pause = true;
            }
            else {
                Time.timeScale = 1;
                pause = false;
            }
        }
    }
}
