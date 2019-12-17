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
    public Canvas endScreen;

    public GameObject player;

    private float restartTimer = 3f;

    private bool livesLeft = true;

    void Awake() {
        endScreen.GetComponent<Menu>().currOption = Menu.Option.FromBeginning;
        pauseMenu.GetComponent<Menu>().currOption = Menu.Option.OtherOption;
    }

    void Update() {
        Pause();
        if (pause) {
            PauseSelectOption();
        }

        //CheckLives() will return true if the player has lives left. False if not.
        if (!CheckLives()) {
            Time.timeScale = 0;
            endScreen.gameObject.SetActive(true);
            EndSelectOption();
        }
    }

    //function to detect when the player presses return/enter and execute the action of the option it's on 
    //(continue or restart)
    public void PauseSelectOption() {
        //KeyCode for selecting choices is return 
        if (Input.GetKeyDown(KeyCode.Return)) {
            //restart the level
            if (pauseMenu.GetComponent<Menu>().GetOption() == Menu.Option.FromBeginning) {
                Debug.Log("Restarting the level");
                SceneManager.LoadScene(1);
                Time.timeScale = 1;
            }

            //select the other option
            else if (pauseMenu.GetComponent<Menu>().GetOption() == Menu.Option.OtherOption) {
                Debug.Log("Returning to main menu");
                pauseMenu.gameObject.SetActive(false);
                Time.timeScale = 1;
                pause = false;
            }
        }
    }

    public void EndSelectOption() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            //retry the game
            if (endScreen.GetComponent<Menu>().GetOption() == Menu.Option.FromBeginning) {
                Debug.Log("Restarting level");
                player.gameObject.GetComponent<HealthManager>().GameRestart();
                SceneManager.LoadScene(1);
                Time.timeScale = 1;
            }

            //select the other option which in this case is return to the mainMenu
            else if (endScreen.GetComponent<Menu>().GetOption() == Menu.Option.OtherOption) {
                Debug.Log("Returning to main menu");
                SceneManager.LoadScene(0);
            }
        }
    }

    //function to pause in-game
    public void Pause() {
        if (Input.GetKeyDown(KeyCode.Escape)) {
            if (!pause) {
                pauseMenu.gameObject.SetActive(true);
                Time.timeScale = 0;
                pause = true;
            }
        }
    }

    private bool CheckLives() {
        return player.gameObject.GetComponent<HealthManager>().LifeCheck();
    }
}
