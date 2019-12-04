using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private bool pause = false;

    public Canvas pauseMenu;

    void Update()
    {
        SelectOption();
        Pause();
    }

    public void SelectOption() {
        if (Input.GetKeyDown(KeyCode.Return)) {
            if (pauseMenu.GetComponent<PauseMenu>().GetOption() == PauseMenu.Option.Retry) {
                SceneManager.LoadScene(0);
                Time.timeScale = 1;
            }

            else if (pauseMenu.GetComponent<PauseMenu>().GetOption() == PauseMenu.Option.Continue) {
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
                Time.timeScale = 0;
                pause = true;
            }
            // else {
            //     pauseMenu.gameObject.SetActive(false);
            //     Time.timeScale = 1;
            //     pause = false;
            // }
        }
    }
}
