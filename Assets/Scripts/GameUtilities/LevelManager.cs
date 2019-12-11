using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

//usage: put this on an empty game object or object that will never be destroyed
//intent: allow the player to pause and restart the level

public class LevelManager : MonoBehaviour
{
    //boolean to control which state the game is in: paused or unpaused
    private bool pause = false;

    public Canvas pauseMenu;

    void Update() {
        SelectOption();
        Pause();
    }

    //function to detect when the player presses return/enter and execute the action of the option it's on 
    //(continue or restart)
    public void SelectOption() {
        //KeyCode for selecting choices is return 
        if (Input.GetKeyDown(KeyCode.Return)) {
            //restart the level
            if (pauseMenu.GetComponent<Menu>().GetOption() == Menu.Option.FromBeginning) {
                SceneManager.LoadScene(1);
                Time.timeScale = 1;
            }

            //continue the game
            else if (pauseMenu.GetComponent<Menu>().GetOption() == Menu.Option.OtherOption) {
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
                pause = false;
            }
        }
    }

    //function to pause in-game
    public void Pause() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!pause) {
                pauseMenu.gameObject.SetActive(true);
                pauseMenu.GetComponent<Menu>().currOption = Menu.Option.OtherOption;
                Time.timeScale = 0;
                pause = true;
            }
        }
    }
}
